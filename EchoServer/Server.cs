using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.TcpServer;

namespace EchoServer
{
    internal class Server : AbstractTcpServer
    {
        public override void TcpServerWork(StreamWriter writer, StreamReader reader)
        {
            string? line = reader.ReadLine();
            writer.AutoFlush = true;
            writer.WriteLine(line);
        }
        /// <summary>
        /// Creates an instance of a TCPServer.
        /// </summary>
        /// <param name="port">Server Port</param>
        /// <param name="name">Server Name</param>
        public Server(int port, string name) : base(port, name)
        {
        }
    }
}
