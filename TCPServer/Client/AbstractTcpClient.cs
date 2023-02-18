using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.TcpClient
{
    public abstract class AbstractTcpClient
    {
        private int _port;
        private string _ip;

        public AbstractTcpClient(int port, string ip)
        {
            _port = port;
            _ip = ip;
        }

        public abstract void DoStuff(StreamWriter writer, StreamReader reader);

        public void Connect()
        {
            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            client.Connect(_ip, _port);
            using (var ns = client.GetStream())
            {
                StreamWriter writer = new StreamWriter(ns);
                StreamReader reader = new StreamReader(ns);
                DoStuff(writer, reader);

            }
        }
    }
}
