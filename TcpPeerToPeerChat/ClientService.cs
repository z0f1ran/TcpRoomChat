using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpPeerToPeerChat
{
    internal class ClientService: IDisposable
    {
        private Socket client;
        private Communication communication;
        public Communication GetCommunication() { return communication; }

        public void Dispose()
        {
            client?.Dispose();
        }

        public ClientService(string ip, int port)
        {
            // 1. создать сокет клиента
            client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.IP);

            // 2. подключится к серверу
            IPAddress serverIp = IPAddress.Parse(ip);
            IPEndPoint serverEndPoint = new IPEndPoint(serverIp, port);
            client.Connect(serverEndPoint); // подключение к серверу
            // 3.
            communication = new Communication(client);
        }
    }
}
