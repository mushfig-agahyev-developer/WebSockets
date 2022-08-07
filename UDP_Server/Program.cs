using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8081;

            IPEndPoint _serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(_serverEndPoint);

            while (true)
            {
                byte[] _bunker = new byte[256];
                int sizePacket = 0;
                StringBuilder data = new StringBuilder();

                EndPoint _senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
                do 
                {
                    sizePacket = _socket.ReceiveFrom(_bunker, ref _senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(_bunker));
                }
                while (_socket.Available > 0);

                _socket.SendTo(Encoding.UTF8.GetBytes("Message received"), _senderEndPoint);
                Console.WriteLine(data);
                Console.ReadLine();
            }
        }
    }
}
