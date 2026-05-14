using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MainProgram.Tasks;

class Task4
{
    public static void Run()
    {
        // 1. Створення сокета
        Socket serverSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
        );

        // 2. Прив’язка до порту 5000
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 5000);
        serverSocket.Bind(endPoint);

        // 3. Початок прослуховування
        serverSocket.Listen(10);

        Console.WriteLine("Сервер запущено на порту 5000...");
        Console.WriteLine("Очікування клієнта...");

        // 4. Очікування клієнта
        Socket clientSocket = serverSocket.Accept();
        Task5.Run();

        // Отримання адреси клієнта
        IPEndPoint clientEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;

        Console.WriteLine($"Клієнт підключився: {clientEndPoint.Address}:{clientEndPoint.Port}");

        // 5. Отримання даних
        byte[] buffer = new byte[1024];
        int bytesReceived = clientSocket.Receive(buffer);

        string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

        Console.WriteLine($"Отримано ({bytesReceived} байт): {message}");

        // 6. Відправка відповіді
        string response = "Повідомлення отримано!";
        byte[] responseData = Encoding.UTF8.GetBytes(response);

        clientSocket.Send(responseData);

        Console.WriteLine("Відправлено відповідь клієнту");

        // 7. Закриття з'єднання
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();

        serverSocket.Close();

        Console.WriteLine("З'єднання закрито");
    }
}