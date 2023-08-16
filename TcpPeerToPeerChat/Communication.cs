using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpPeerToPeerChat
{
    public class Communication
    {
        private Socket socket;
        private IPAddress clientIPAddress;
        public Communication(Socket socket) 
        { 
            this.socket = socket;
            this.clientIPAddress = ((IPEndPoint)socket.RemoteEndPoint).Address;
        }
        public IPAddress GetClientIPAddress()
        {
            return clientIPAddress;
        }
        public void SendMessage(string message)
        {
            // отправить сообщение клиенту от сервера
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            socket.Send(buffer);
        }
        public void CloseConnection()
        {
            socket.Close();
    
        }
        public string ReceiveMessage()
        {
            // получить сообщение от севера клиентом
            if (socket.Poll(1000, SelectMode.SelectRead))
            {
                byte[] buffer = new byte[1024];            
                int received_bytes_count = socket.Receive(buffer);
                return Encoding.UTF8.GetString(buffer, 0, received_bytes_count);
            }
            return null;
        }
    }
}
