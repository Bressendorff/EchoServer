using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.TcpClient;

namespace EchoServer
{
    internal class Client : AbstractTcpClient
    {
        public Client(int port, string ip) : base(port, ip)
        {
        }

        public override void DoStuff(StreamWriter writer, StreamReader reader)
        {
            string? message = Console.ReadLine();
            writer.WriteLine(message);
            writer.Flush();

            string? response = reader.ReadLine().ToUpper();
            Console.WriteLine(response);
        }
    }
}
