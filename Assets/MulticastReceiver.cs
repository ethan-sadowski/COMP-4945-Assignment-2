using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using SnakeBehaviour;
using UnityEngine.SceneManagement;
using SnakeMovementController;
using SnakeCreation;

namespace MulticastReceive
{
    public class MulticastReceiver : MonoBehaviour
    {
        public SnakeMovement snakeMovement;
        public SnakeCreator snakeCreator;
        private static IPAddress mcastAddress;
        private static int mcastPort;
        private static Socket mcastSocket;
        private static MulticastOption mcastOption;
        private Guid id;
        IPAddress localIP;
        EndPoint localEP;
        IPEndPoint groupEP;
        EndPoint remoteEP;
        Thread socketThread;
        bool socketThreadRunning;
        Tuple<Guid, List<Vector2>> newSnakeData;
        List<Vector2> coordinateList;
        Guid parsedUid;
        // Start is called before the first frame update
        void Start()
        {
            mcastAddress = IPAddress.Parse("230.0.0.1");
            mcastPort = 11000;
            mcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            localIP = IPAddress.Any;
            localEP = (EndPoint)new IPEndPoint(localIP, mcastPort);
            mcastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            mcastSocket.Bind(localEP);
            mcastOption = new MulticastOption(mcastAddress, localIP);
            mcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, mcastOption);
            groupEP = new IPEndPoint(mcastAddress, mcastPort);
            remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
            socketThread = new Thread(listen);
            socketThreadRunning = true;
            socketThread.Start();
        }

        public void setId(Guid id)
        {
            this.id = id;
        }

        void listen()
        {
            while (socketThreadRunning)
            {
                try
                {

                    byte[] bytes = new Byte[130];

                    mcastSocket.ReceiveFrom(bytes, ref remoteEP);
                    string snakeInfo = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    //TODO: Add a conditional check to see what type of message the broadcast is (snake movement / apple locations).

                    // Parse x coordinate of the snake
                    int xStart = snakeInfo.IndexOf("xcoordinate: ") + 13;
                    int xEnd = snakeInfo.IndexOf("---end-x---");
                    int xcoordinate = int.Parse(snakeInfo.Substring(xStart, xEnd - xStart));

                    // Parse y coordinate of the snake
                    int yStart = snakeInfo.IndexOf("ycoordinate: ") + 13;
                    int yEnd = snakeInfo.IndexOf("---end-y---");
                    float ycoordinate = float.Parse(snakeInfo.Substring(yStart, yEnd - yStart));

                    // Parse UID of the snake
                    int uidStart = snakeInfo.IndexOf("uid: ") + 5;
                    int uidEnd = snakeInfo.IndexOf("---end-uid---");
                    string uid = snakeInfo.Substring(uidStart, uidEnd - uidStart);

                    parsedUid = Guid.Parse(uid);
                    // If the snake is a new connection create a new snake
                    bool isNewSnake = !snakeMovement.checkIfSnakeExists(parsedUid);

                    // TODO refactor this to create a list of all the snake's coordinates
                    coordinateList = new List<Vector2>();
                    coordinateList.Add(new Vector2(xcoordinate, ycoordinate));

                    if (isNewSnake)
                    {


                        Debug.Log(snakeInfo);
                        Debug.Log("new");

                        newSnakeData = new Tuple<Guid, List<Vector2>>(parsedUid, coordinateList);
                        socketThreadRunning = false;
                        Debug.Log(newSnakeData);
                        Debug.Log(socketThreadRunning);

                    }
                    else if (uid != this.id.ToString())
                    {


                        mainThreadUpdate();
                    }

                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    Console.WriteLine(e.ToString());
                    socketThread.Abort();
                    mcastSocket.Close();
                }
                
            }
            Debug.Log("test");
            Debug.Log(socketThreadRunning);
            socketThread.Abort();

        }

        // Update is called once per frame
        void Update()
        {
            if (!socketThreadRunning)
            {
                Debug.Log("rerun?");
                GameObject newSnake = snakeCreator.instantiateSnake(newSnakeData.Item1, newSnakeData.Item2);
                snakeMovement.addSnake(newSnake);
                socketThread = new Thread(listen);
                socketThreadRunning = true;
                socketThread.Start();
            }
        }

        ~MulticastReceiver()
        {
            mcastSocket.Close();
        }

        public IEnumerator functionExecution()
        {
            snakeMovement.updateSnakeLocation(parsedUid, coordinateList);
            yield return null;
        }

        public void mainThreadUpdate()
        {
            UnityMainThreadDispatcher.Instance().Enqueue(functionExecution());
        }

    }
}