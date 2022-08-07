using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace TCP_Client
{
    class Program
    {
        static  void Main(string[] args)
        {
            const string ip = "127.0.0.1";
            const int port = 8080;

            IPEndPoint _tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //for the TCP using Stream
            Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Enter message:");
            var _message = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(_message);
            _socket.Connect(_tcpEndPoint);
            _socket.Send(data);

            byte[] _bunker = new byte[256];
            var sizePacket = 0;
            StringBuilder answer = new StringBuilder();

            do
            {
                sizePacket = _socket.Receive(_bunker);
                answer.Append(Encoding.UTF8.GetString(_bunker, 0, sizePacket));
            }
            while (_socket.Available > 0);

            Console.WriteLine(answer.ToString());

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();

            Console.ReadLine();
        }
    }
}
