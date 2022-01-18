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
    //Worked on By Christopher Spooner, Ethan Sadowski, and Sam Shannon
    public class MulticastSender : MonoBehaviour
    {
        static IPAddress mcastAddress;
        static int mcastPort;
        static Socket mcastSocket;
        private Guid id;
        IPEndPoint endPoint;

        void Start()
        {

            mcastAddress = IPAddress.Parse("230.0.0.1");
            mcastPort = 11000;
         
        }

        public void send(string snakeInfo)
        {
            try
            {
                mcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                endPoint = new IPEndPoint(mcastAddress, mcastPort);
                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes(snakeInfo), endPoint);

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
