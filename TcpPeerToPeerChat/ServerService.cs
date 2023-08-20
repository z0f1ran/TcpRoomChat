using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TcpPeerToPeerChat
{
    internal class ServerService: IDisposable
    {
        private Socket server;
        private List<Communication> clientList = new List<Communication>();
        private Communication communication = null;
        public Communication GetCommunication() { return communication; }
        public ServerService(string ip, int port)
        {
            // 1. Создать сокет
            server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.IP);
            // 2. Связать сокет с endpoint-ом на хосте (ip:порт)
            IPAddress serverIp = IPAddress.Parse(ip);
            IPEndPoint serverEndPoint = new IPEndPoint(serverIp, port);
            server.Bind(serverEndPoint);
            // 3.
            server.Listen(10);
        }

        public void StartServer()
        {
            while (true)
            {
                Communication client = new Communication(server.Accept());
                if (client != null)
                {
                    clientList.Add(client);

                    Thread clientThread = new Thread(ClientThreadFunction);
                    clientThread.Start(client);
                }
            }
        }

        private void ClientThreadFunction(object clientObject)
        {
            Communication client = (Communication)clientObject;

            try
            {
                SendToAll("Client connected: " + (client.GetClientIPAddress()));

                while (true)
                {
                    string message = client.ReceiveMessage(); // Ожидание сообщения от клиента
                    if (message == null)
                    {
                        // Клиент отключился
                        clientList.Remove(client);
                        SendToAll("Client disconnected: " + (client.GetClientIPAddress()));
                        break;
                    }

                    // Отправка полученного сообщения остальным клиентам
                    SendToAll(message);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если клиент или соединение неожиданно закрылось
                MessageBox.Show($"Client thread error: {ex.Message}");
            }
        }
        public void SendToAll(string message)
        {
            foreach (Communication client in clientList)
            {
                try
                {
                    if (client != null)
                    {
                        client.SendMessage(message);
                    }       
                }
                catch (Exception ex)
                {
                    // Обрабатываем любые исключения, которые могут возникнуть при отправке клиенту.
                    MessageBox.Show($"Ошибка при отправке сообщения клиенту: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            server?.Close();
            foreach(Communication client in clientList)
            {
                client?.CloseConnection();
            }
            
        }
    }
}
