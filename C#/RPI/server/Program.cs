using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace piServer
{
    class Util
    {
        public static void SendMessage(string message, NetworkStream stream)
        {
            // Convert message to bytes
            byte[] outBuffer = Encoding.ASCII.GetBytes(message);

            // Send using TCP
            stream.Write(outBuffer, 0, outBuffer.Length);

            // Confirm send
            Console.WriteLine("MESSAGE SENT ON STREAM " + stream + ":");
            Console.WriteLine(message + "\n-----------------------------------");
        }

        public static string RecieveMessage(NetworkStream stream)
        {
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

    class Program
    {
        static void Main(string[] args) {
            // Set up and start server
            IPAddress ipAddress = IPAddress.Parse("10.10.5.12");  // THIS HAS TO RUN ON PI, OR CHANGE TO 127.0.0.1 FOR LOCAL TESTING
            TcpListener server = new TcpListener(ipAddress, 8080);
            server.Start();
            Console.WriteLine("Started server on " + server.LocalEndpoint);

            // Accept connection 1
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Accepted connection!"); Console.ForegroundColor = ConsoleColor.White;
            TcpClient c1 = server.AcceptTcpClient();
            NetworkStream c1_stream = c1.GetStream();

            // Accept connection 2
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Accepted connection 2!"); Console.ForegroundColor = ConsoleColor.White;
            TcpClient c2 = server.AcceptTcpClient();
            NetworkStream c2_stream = c2.GetStream();

            // NOTE: This code probably wont work, but we'll give it a go anyway...
            // Might need to put it in a while loop to keep it running if it only does one message

            Task.Run(() =>
            {
                // Async task for CLIENT 1
                // Listen for messages from client 2 and echo to client 1
                string msg = Util.RecieveMessage(c2_stream);
                Util.SendMessage(msg, c1_stream);
            });

            Task.Run(() =>
            {
                // Async task for CLIENT 2
                // Listen for messages from client 1 and echo to client 2
                string msg = Util.RecieveMessage(c1_stream);
                Util.SendMessage(msg, c2_stream);
            });

            // Close down shiz
            c1_stream.Close();
            c2_stream.Close();
            c1.Close();
            c2.Close();
            server.Stop();
        }
    }
}