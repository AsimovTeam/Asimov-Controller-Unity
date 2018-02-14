using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace AsimovController
{
    class Program
    {
        private static Socket socket;

        static void Main(string[] args)
        {
            String hostIP = "127.0.0.1";
            int hostPort = 44000;

            IPEndPoint host = new IPEndPoint(IPAddress.Parse(hostIP), hostPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(host);

            NetworkStream stream = new NetworkStream(socket);
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            writer.WriteLine("{ \"message\" : \" " + "Hello World!" + "\" }");

            Boolean done = false;

            while(!done)
            {
                switch(Console.ReadKey().KeyChar)
                {
                    case 'w':
                        writer.WriteLine("{ \"message\" : \" " + "Pressed W!" + "\" }");
                        continue;
                    case 'a':
                        writer.WriteLine("{ \"message\" : \" " + "Pressed A!" + "\" }");
                        continue;
                    case 's':
                        writer.WriteLine("{ \"message\" : \" " + "Pressed S!" + "\" }");
                        continue;
                    case 'd':
                        writer.WriteLine("{ \"message\" : \" " + "Pressed D!" + "\" }");
                        continue;
                    case 'q':
                        writer.WriteLine("{ \"message\" : \" " + "Done!" + "\" }");
                        done = true;
                        continue;
                }
            }
            
            socket.Disconnect(false);
        }
    }
}
