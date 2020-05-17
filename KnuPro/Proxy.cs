/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System;
using System.Net;
using System.Net.Sockets;

namespace KnuPro {
    class Proxy {
        private IPEndPoint Local;
        private IPEndPoint Remote;
        private Socket Server;

        public Proxy(string ip_address, int ip_port) {
            this.Local  = new IPEndPoint(IPAddress.Parse("127.0.0.1"), ip_port);
            this.Remote = new IPEndPoint(IPAddress.Parse(ip_address), ip_port);
            this.Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            Run();
        }

        private void Run() {
            Console.WriteLine("Start Service for <" + Remote.Address + ":" + Remote.Port + "> on Port " + Local.Port);
            Debugger.Clear();

            this.Server.Bind(Local);
            this.Server.Listen(10);

            while(true) {
                new Session(this.Remote, this.Server.Accept()).Start();
            }
        }
    }
}
