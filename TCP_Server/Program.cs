using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;
            IPEndPoint _tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //for the TCP using Stream
            Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(_tcpEndPoint);
            _socket.Listen(10);

            StringBuilder _message = new StringBuilder();

            while (true)
            {
                Socket _listener = _socket.Accept();
                byte[] _bunker = new byte[256];
                int sizePacket = 0;

                do
                {
                    sizePacket = _listener.Receive(_bunker);
                    _message.Append(Encoding.UTF8.GetString(_bunker, 0, sizePacket));
                }
                while (_listener.Available > 0);

                Console.WriteLine(_message);

                _listener.Send(Encoding.UTF8.GetBytes("Status:200"));
                _listener.Shutdown(SocketShutdown.Both);
                _listener.Close();
            }
        }
    }
}
