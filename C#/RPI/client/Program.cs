using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client 
{
    class Util
    {
        public static void SendMessage(string message, NetworkStream stream) {
            // Encode message to bytes
            byte[] sendBuffer = Encoding.ASCII.GetBytes(message);

            // Send message
            stream.Write(sendBuffer, 0, sendBuffer.Length);

            // Confirm sending
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Sent message: " +  message + "\n--------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void RecieveMessage(NetworkStream stream) {
            // Listen for message
            byte[] recieveBuffer = new byte[1024];
            int readBytes = stream.Read(recieveBuffer, 0, recieveBuffer.Length);

            // Decode message
            string message = Encoding.ASCII.GetString(readBytes);

            // Confirm message recieval
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Recieved message: " + message + "\n--------------------------");
        }
    }

    class Program 
    {
        // CONSTANTS
        string IP = "10.10.5.12";  // NOTE: 10.10.5.12 is RPI4B IP, change this to 127.0.0.1 for local testing...

        static void Main(string[] args) {
            Console.WriteLine("Hello! I am a client but im not quite done yet...");

            // Set up client connection
            TcpClient client = new TcpClient();
            client.Connect(IP), 8080);
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Connected to server! " + client.Client.RemoteEndPoint); Console.ForegroundColor= ConsoleColor.White;

            // Create stream for reading and writing data
            NetworkStream dataStream = client.GetStream();

            Console.WriteLine("Type messages to send...");

            // Set up reading and writing tasks
            Task.Run(() =>
            {
                // Send messages
                string msg = Console.ReadLine();
                Util.SendMessage(msg, dataStream);

                if (msg == "SERVER.EXIT")
                {
                    break;
                }
            });

            Task.Run(() => {Util.RecieveMessage(dataStream);});

            // Close all connections
            dataStream.Close();
            client.Close();
        }
    }
}