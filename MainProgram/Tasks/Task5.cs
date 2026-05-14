using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainProgram.Tasks;

class Task5
{
    public static void Run()
    {
        // 1. Створити клієнтський сокет
        Socket clientSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        // 2. Адреса сервера
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);

        Console.WriteLine("Підключення до сервера 127.0.0.1:5000...");

        // 3. Підключення
        clientSocket.Connect(serverEndPoint);

        Console.WriteLine("Підключено!");

        // 4. Повідомлення
        string message = "Привіт, сервер!";
        byte[] data = Encoding.UTF8.GetBytes(message);

        // 5. Відправка
        clientSocket.Send(data);

        Console.WriteLine("Відправлено: " + message);

        // 6. Отримання відповіді
        byte[] buffer = new byte[1024];
        int bytesReceived = clientSocket.Receive(buffer);

        string response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

        Console.WriteLine("Отримано відповідь: " + response);

        // 7. Закриття з'єднання
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();

        Console.WriteLine("З'єднання закрито");
    }
}