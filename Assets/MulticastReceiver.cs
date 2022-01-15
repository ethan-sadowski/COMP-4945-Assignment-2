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


namespace MulticastReceive
{
    public class MulticastReceiver : MonoBehaviour
    {
        private static IPAddress mcastAddress;
        private static int mcastPort;
        private static Socket mcastSocket;
        private static MulticastOption mcastOption;
        private Guid id;
        IPAddress localIP;
        EndPoint localEP;
        IPEndPoint groupEP;
        EndPoint remoteEP;

/*        public MulticastReceiver(Guid id) {
            this.id = id;
        }
*/

        // Start is called before the first frame update
        void Start()
        {
            mcastAddress = IPAddress.Parse("230.0.0.1");
            mcastPort = 11000;
            mcastSocket = new Socket(AddressFamily.InterNetwork,
                                SocketType.Dgram,
                                ProtocolType.Udp);
            localIP = IPAddress.Any;
            localEP = (EndPoint)new IPEndPoint(localIP, mcastPort);
            mcastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            mcastSocket.Bind(localEP);


            // Define a MulticastOption object specifying the multicast group 
            // address and the local IPAddress.
            // The multicast group address is the same as the address used by the server.
            mcastOption = new MulticastOption(mcastAddress, localIP);

            mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                        SocketOptionName.AddMembership,
                                        mcastOption);


            groupEP = new IPEndPoint(mcastAddress, mcastPort);
            remoteEP = (EndPoint)new IPEndPoint(IPAddress.Any, 0);

            
        }

        // Update is called once per frame
        void Update()
        {
            try
            {

                /* byte[] bytes = new Byte[100];

                 Console.WriteLine("Waiting for multicast packets.......");
                 mcastSocket.ReceiveFrom(bytes, ref remoteEP);
                 string snakeInfo = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                 //Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                 //remoteEP.ToString(),
                 //snakeInfo);

                 //TODO: Add a conditional check to see what type of message the broadcast is (update position, delete snake (aka. snake died), etc.)

                 // Parse x coordinate of the snake
                 int xStart = snakeInfo.IndexOf("xcoordinate: ") + 13;
                 int xEnd = snakeInfo.IndexOf("---end-x---");
                 int xcoordinate = int.Parse(snakeInfo.Substring(xStart, xEnd - xStart));

                 // Parse y coordinate of the snake
                 int yStart = snakeInfo.IndexOf("ycoordinate: ") + 13;
                 int yEnd = snakeInfo.IndexOf("---end-y---");
                 int ycoordinate = int.Parse(snakeInfo.Substring(yStart, yEnd - yStart));

                 // Parse UID of the snake
                 int uidStart = snakeInfo.IndexOf("uid: ") + 5;
                 int uidEnd = snakeInfo.IndexOf("---end-uid---");
                 string uid = snakeInfo.Substring(uidStart, uidEnd - uidStart);

                 // Instantiate 2 demo Snakes and add them to the snakes List
                 *//*                    List<Snake> snakes = new List<Snake>(); // Don't instantiate this every time in final product. Bring out of while loop
                                     Snake s1 = new Snake(0, 0, "s1");
                                     Snake s2 = new Snake(1, 1, "s2");
                                     snakes.Add(s1);
                                     snakes.Add(s2);*//*

                // Determine if the snake is a new Connection or if the snake is already in the game
                int snakeIndex = 0;
                bool isNewSnake = true;
                for (int i = 0; i < snakes.Count; i++)
                {
                    //Console.WriteLine(s.uid);
                    if (snakes[i].uid == uid)
                    {
                        isNewSnake = false;
                        snakeIndex = i;
                        break;
                    }
                }
                // If the snake is a new connection create a new snake
                if (isNewSnake)
                {
                    Snake newSnake = new Snake(xcoordinate, ycoordinate, uid);
                    Console.WriteLine("adding new snake: " + newSnake.uid);
                    snakes.Add(newSnake);
                }
                // Else update the existing snake's position
                else
                {
                    Console.WriteLine("updating old snake: " + snakes[snakeIndex].uid);
                    snakes[snakeIndex].x = xcoordinate;
                    snakes[snakeIndex].y = ycoordinate;
                }

                mcastSocket.Close();*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
