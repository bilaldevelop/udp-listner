using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;

            //create TCP server
            ClientText();
            Console.WriteLine("This is a Client, host name is {0}", Dns.GetHostName());

            //setup setver IP，set up TCP port
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8001);

            //set up protocol type ，data connection type and data type UDP
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            string welcome = "Hello! ";
            data = Encoding.ASCII.GetBytes(welcome);
            server.SendTo(data, data.Length, SocketFlags.None, ipep);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            int recv = server.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Message received from {0}: ", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                    break;
                server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
                data = new byte[1024];
                recv = server.ReceiveFrom(data, ref Remote);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            Console.WriteLine("Stopping Client.");
            server.Close();
        }


        private static void ClientText()
        {
            string text = $@"

  _    _ _____  _____     _____ _ _            _   
 | |  | |  __ \|  __ \   / ____| (_)          | |  
 | |  | | |  | | |__) | | |    | |_  ___ _ __ | |_ 
 | |  | | |  | |  ___/  | |    | | |/ _ \ '_ \| __|
 | |__| | |__| | |      | |____| | |  __/ | | | |_ 
  \____/|_____/|_|       \_____|_|_|\___|_| |_|\__|
                                                   
                                                   
                                                                                                         
";

            Console.WriteLine(text);
        }

    }
}

