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

namespace MulticastSend
{
    public class MulticastSender : MonoBehaviour
    {
        static IPAddress mcastAddress;
        static int mcastPort;
        static Socket mcastSocket;
        private Guid id;
        IPEndPoint endPoint;

        // Start is called before the first frame update
        void Start()
        {
            // Initialize the multicast address group and multicast port.
            // Both address and port are selected from the allowed sets as
            // defined in the related RFC documents. These are the same 
            // as the values used by the sender.
            mcastAddress = IPAddress.Parse("230.0.0.1");
            mcastPort = 11000;
         
        }

        public void send(string snakeInfo)
        {
            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork,
                               SocketType.Dgram,
                               ProtocolType.Udp);

                //Send multicast packets to the listener.
                endPoint = new IPEndPoint(mcastAddress, mcastPort);
                String s;
                bool done = false;

                Console.WriteLine(snakeInfo);
                Debug.Log(snakeInfo);
                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes(snakeInfo), endPoint);
/*                while (!done)
                {
                    s = Console.ReadLine();
                    Debug.Log(s);
                    mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes(s), endPoint);
                }*/


            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.ToString());
            }

            mcastSocket.Close();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
