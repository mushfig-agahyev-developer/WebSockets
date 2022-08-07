using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8082;
            IPEndPoint _serverpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(_serverpEndPoint);

            while (true)
            {
                Console.WriteLine("Enter message");
                var _message = Console.ReadLine();

                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
                _socket.SendTo(Encoding.UTF8.GetBytes(_message), serverEndPoint);

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

                Console.WriteLine(data);
           
            }
        }
    }
}
