import java.net.*;
import java.io.*;

public class piServer {
    public static void main(String[] args) throws IOException {
        // Create serversocket and listen for a connection
        ServerSocket serverSocket = new ServerSocket(8080);
        System.out.println("Server online: listening on port 8080...");
        Socket clientSocket = serverSocket.accept();
        String clientIP = clientSocket.getInetAddress().toString();
        System.out.println("Client connected: " + clientIP);

        // Set up data streams
        DataInputStream dataInputStream = new DataInputStream(clientSocket.getInputStream());

        // While connection should be maintained, listen for messages
        String escapeString = "SERVER.EXIT";
        while (true) {
            String message = dataInputStream.readUTF();
            System.out.println("NEW MESSAGE: " + message);

            if (message.equals(escapeString)) {break;}
        }

        // End connection
        System.out.println("Closing server...");
        dataInputStream.close();
        clientSocket.close();
        serverSocket.close();
    }
}