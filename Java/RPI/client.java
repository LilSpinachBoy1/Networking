import java.io.*;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.util.Scanner;

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
        Scanner sc = new Scanner(System.in);

        // Keep connection open while escape string not sent
        String escapeString = "SERVER.EXIT";
        while (true) {
            String message = sc.nextLine();
            dataOut.writeUTF(message);

            if (message.equals(escapeString)) {
                break;
            }
        }

        // Close connections and say goodbye
        System.out.println("Goodbye!");
        socket.close();
        dataOut.close();
        dataIn.close();
    }
}