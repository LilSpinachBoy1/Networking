import java.io.IOException;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.io.Scanner;

public class client {
    public static void main(String[] args) throws IOException {
        String RPI_IP = "10.10.5.12"; // IP FOR RASPBERRY PI (samPi4B.local)

        // Try to connect to server
        Socket socket = new Socket();
        socket.connect(new InetSocketAddress("10.10.5.12", 8080), 1000);
        System.out.println("Successfully connected to " + RPI_IP + "!");

        // Create data streams
        DataInputStream dataIn = new DataInputStream(socket.getInputStream());
        DataOutputStream dataOut = new DataOutputStream(socket.getOutputStream());

        // Keep connection open while escape string not sent
        String escapeString = "SERVER.EXIT";
        do {
            String message = System.
            dataOut.writeUTF(message);
        }
    }
}