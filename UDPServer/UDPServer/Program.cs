using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPServer
{
    class Program
    {
        static void Main(string[] args)
        {

          

            int recv;
            byte[] data = new byte[1024];

            //create TCP server
            ServerText();
            //get local IP，set TCP port         
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 8001);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //binding network address
            newsock.Bind(ipep);

            Console.WriteLine("This is a Server, host name is {0}", Dns.GetHostName());

            //wait client connectiong
            Console.WriteLine("Waiting for a client...");

            //get client IP
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);
            recv = newsock.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Message received from {0}: ", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            //client connected successed after send welcome message
            string welcome = "Welcome ! ";

            // string convert to byte array data
            data = Encoding.ASCII.GetBytes(welcome);

            //send message
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                //send accept message
                recv = newsock.ReceiveFrom(data, ref Remote);
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
        }


        private static void ServerText()
        {
            string text = $@"

 _   _____________   _____                          
| | | |  _  \ ___ \ /  ___|                         
| | | | | | | |_/ / \ `--.  ___ _ ____   _____ _ __ 
| | | | | | |  __/   `--. \/ _ \ '__\ \ / / _ \ '__|
| |_| | |/ /| |     /\__/ /  __/ |   \ V /  __/ |   
 \___/|___/ \_|     \____/ \___|_|    \_/ \___|_|   
                                                    
                                                                                                                                                         
";

            Console.WriteLine(text);
        }

    }
}
