using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Net;

namespace ServeurAsimovSimuler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application pour simuler un serveur Asimov!");

            int port = 8888;
            byte[] addrs = { 127, 0, 0, 1 };

            TcpListener serverSocket = new TcpListener(new IPAddress(addrs), port);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();

            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started");
                HandleClient client = new HandleClient();
                client.StartClient(clientSocket, Convert.ToString(counter));
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");


            Console.ReadKey(true);
        }
    }

    public class HandleClient
    {
        TcpClient clientSocket;
        string clNo;

        public void StartClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(DoChat);
            ctThread.Start();
        }

        private void DoChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[70025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while (clientSocket.Connected)
            {
                try
                {

                    requestCount++;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    //dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("/"));
                    dataFromClient = dataFromClient.Replace("\0", ""); // Pour retirer les \0 qui sont rajouté pour une raison inconnu.
                    Console.WriteLine(" >> " + "From client [" + clNo + "] >> " + dataFromClient);

                    rCount = Convert.ToString(requestCount);
                    serverResponse = "Server to clinet(" + clNo + ") " + rCount;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.Message);
                }
            }
        }
    }
}
