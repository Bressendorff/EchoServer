using System.Net;
using System.Net.Sockets;

namespace TCPServer.TcpServer
{
    /// <summary>
    /// Abstract TCPServer
    /// </summary>
    public abstract class AbstractTcpServer
    {
        private bool _stop = false;
        private int _port;
        private string _name;
        private List<Task> _tasks = new List<Task>();

        /// <summary>
        /// Creates an instance of the server. 
        /// </summary>
        /// <param name="port">Server Port Number</param>
        /// <param name="name">Server Name</param>
        protected AbstractTcpServer(int port, string name)
        {
            _port = port;
            _name = name;
        }

        private void HandleClient(System.Net.Sockets.TcpClient socket)
        {
            using (var ns = socket.GetStream())
            {
                var writer = new StreamWriter(ns);
                var reader = new StreamReader(ns);
                TcpServerWork(writer, reader);
            }
        }

        /// <summary>
        /// Abstract method that defines the communication protocol when handling clients. 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="reader"></param>
        public abstract void TcpServerWork(StreamWriter writer, StreamReader reader);

        /// <summary>
        /// Starts the server, listening for clients.
        /// </summary>
        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            Console.WriteLine($"Started server {_name}, PORT: {_port}");
            listener.Start();
            Task.Run(() =>
            {
                Stop();
            });
            while (!_stop)
            {
                if (listener.Pending())
                {
                    System.Net.Sockets.TcpClient client = listener.AcceptTcpClient();
                    _tasks.Add(
                        Task.Run(() =>
                            {
                                System.Net.Sockets.TcpClient tempSocket = client;
                                HandleClient(tempSocket);
                            }
                        ));
                }
                else
                {
                    Thread.Sleep(1000);
                }
                
                
            }

            Task.WaitAll(_tasks.ToArray());
        }

        private void Stop()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, ++_port);
            listener.Start();
            System.Net.Sockets.TcpClient client = listener.AcceptTcpClient();
            _stop = true;
        }


    }
}