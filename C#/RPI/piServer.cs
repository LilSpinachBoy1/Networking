using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace piServer {
    class Program {
        static void Main(string[] args) {
            // Set up and start server
            IPAddress ipAddress = IPAddress.Parse("10.10.5.12");
            TcpListener server = new TcpListener(ipAddress, 8080);
            server.Start();
            Console.WriteLine("Started server on " + server.LocalEndpoint());

            // Accept 2 connections
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Accepted connection!"); Console.ForegroundColor = ConsoleColor.White;
            TcpClient c1 = server.AcceptTcpClient();
            NetworkStream c1_stream = c1.GetStream();


            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Accepted connection 2!"); Console.ForegroundColor = ConsoleColor.White;
            TcpClient c2 = server.AcceptTcpClient();
            NetworkStream c2_stream = c2.GetStream();
        }

        public void SendMessage(string message, NetworkStream stream) {
            // Convert message to bytes
            byte[] outBuffer = Encoding.ASCII.GetBytes(message);

            // Send using TCP
            stream.Write(outBuffer, 0, outBuffer.Length);

            // Confirm send
            Console.WriteLine("MESSAGE SENT ON STREAM " + stream + ":");
            Console.WriteLine(message + "\n-----------------------------------");
        }

        public string RecieveMessage(NetworkStream stream) {
            // Listen for incoming message
            byte[] inBuffer = new byte[1024];
            int inBytes = stream.Read(inBuffer, 0, inBuffer.Length);

            // Decode message
            string message = Encoding.ASCII.GetString(inBuffer, 0, inBytes);

            // Confirm message recieved
            Console.WriteLine("MESSAGE RECIEVED FROM " + stream + ":");
            Console.WriteLine(message + "\n-----------------------------------");

            // Return decoded message
            return message;
        }
    }
}