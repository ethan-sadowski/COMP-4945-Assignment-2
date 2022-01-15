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

        }

        // Update is called once per frame
        void Update()
        {
            try
            {

                byte[] bytes = new Byte[130];

                mcastSocket.ReceiveFrom(bytes, ref remoteEP);
                string snakeInfo = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Debug.Log(snakeInfo);
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

                Guid parsedUid = Guid.Parse(uid);
                // If the snake is a new connection create a new snake
                bool isNewSnake = !snakeMovement.checkIfSnakeExists(parsedUid);
                Debug.Log(isNewSnake);
                if (isNewSnake)
                {
                    Debug.Log("new");
                    snakeCreator.instantiateSnake(parsedUid);
                    
                }

                mcastSocket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
