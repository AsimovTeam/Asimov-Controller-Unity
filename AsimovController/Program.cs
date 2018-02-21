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
            int hostPort = 8888;

            IPEndPoint host = new IPEndPoint(IPAddress.Parse(hostIP), hostPort);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(host);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue lors de la connection!\nMessage : " + ex.Message);
            }

            if (socket.Connected)
            {
                Console.WriteLine("Connection réussi!");
                Console.WriteLine("Information de connection : \n >> " + socket.AddressFamily);

                NetworkStream stream = new NetworkStream(socket);
                StreamWriter writer = new StreamWriter(stream)
                {
                    AutoFlush = true
                };

                writer.WriteLine("{ \"message\" : \" " + "Hello World!" + "\" }");

                Boolean done = false;

                while (!done)
                {
                    switch (Console.ReadKey().KeyChar)
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

                try
                {
                    socket.Close();
                    //socket.Disconnect(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Une erreur est survenue lors de la tentative de déconnection. Message : " + ex.ToString());
                }

            }

            Console.ReadLine();


        }
    }
}
