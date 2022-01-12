using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

// This sender example must be used in conjunction with the listener program.
// You must run this program as follows:
// Open a console window and run the listener from the command line. 
// In another console window run the sender. In both cases you must specify 
// the local IPAddress to use. To obtain this address,  run the ipconfig command 
// from the command line. 
//  

public class SenderThread {

    static IPAddress mcastAddress;
    static int mcastPort;
    static Socket mcastSocket;

    public static void run() {
        // Initialize the multicast address group and multicast port.
        // Both address and port are selected from the allowed sets as
        // defined in the related RFC documents. These are the same 
        // as the values used by the sender.
        mcastAddress = IPAddress.Parse("230.0.0.1");
        mcastPort = 11000;
        IPEndPoint endPoint;

        try {
            mcastSocket = new Socket(AddressFamily.InterNetwork,
                           SocketType.Dgram,
                           ProtocolType.Udp);

            //Send multicast packets to the listener.
            endPoint = new IPEndPoint(mcastAddress, mcastPort);
            String s;
            bool done = false;
            while(!done) {
                s = Console.ReadLine();
                mcastSocket.SendTo(ASCIIEncoding.ASCII.GetBytes(s), endPoint);
            }
            
        } catch(Exception e) {
            Console.WriteLine("\n" + e.ToString());
        }

        mcastSocket.Close();
    }
}

public class ReceiverThread {
    private static IPAddress mcastAddress;
    private static int mcastPort;
    private static Socket mcastSocket;
    private static MulticastOption mcastOption;

    public static void run() {
        mcastAddress = IPAddress.Parse("230.0.0.1");
        mcastPort = 11000;


        try {
            mcastSocket = new Socket(AddressFamily.InterNetwork,
                         SocketType.Dgram,
                         ProtocolType.Udp);
            IPAddress localIP = IPAddress.Any;
            EndPoint localEP = (EndPoint) new IPEndPoint(localIP, mcastPort);


            mcastSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            mcastSocket.Bind(localEP);


            // Define a MulticastOption object specifying the multicast group 
            // address and the local IPAddress.
            // The multicast group address is the same as the address used by the server.
            mcastOption = new MulticastOption(mcastAddress, localIP);

            mcastSocket.SetSocketOption(SocketOptionLevel.IP,
                                        SocketOptionName.AddMembership,
                                        mcastOption);


            bool done = false;
            byte[] bytes = new Byte[100];
            IPEndPoint groupEP = new IPEndPoint(mcastAddress, mcastPort);
            EndPoint remoteEP = (EndPoint) new IPEndPoint(IPAddress.Any, 0);

            while(!done) {
                Console.WriteLine("Waiting for multicast packets.......");

                mcastSocket.ReceiveFrom(bytes, ref remoteEP);

                Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                  remoteEP.ToString(),
                  Encoding.ASCII.GetString(bytes, 0, bytes.Length));


            }

            mcastSocket.Close();
        } catch(Exception e) {
            Console.WriteLine(e.ToString());
        }
    }
}

class TestMulticastOptionSender {



    static void Main() {

        Thread receiver = new Thread(ReceiverThread.run);
        Thread sender = new Thread(SenderThread.run);
        sender.Start();
        receiver.Start();
        


    }
}
