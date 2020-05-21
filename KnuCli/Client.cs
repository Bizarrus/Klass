/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Networking;
using KnuCli.UI;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using Klass.Helper;
using KnuCli.Model;
using System.Collections.Generic;
using System.Windows.Threading;

namespace KnuCli {
    public class Client : IClient {
        private App core;
        private string hostname         = "";
        private int port                = -1;
        private byte[] key_connection   = null;
        private string hash_password;
        private Proxy proxy;
        private Login login;
        private TcpClient socket;
        protected Huffman huffman;
        protected Protocols protocols;
        private Dictionary<string, ChannelFrame> windows = new Dictionary<string, ChannelFrame>();

        public Client(App core, string hostname, int port) {
            this.core       = core;
            this.huffman    = new Huffman();
            this.protocols  = new Protocols(this);
            this.login      = new Login(this);
            this.hostname   = hostname;
            this.port       = port;
        }

        public void CreateChannelFrame(string name, Action<ChannelFrame> callback) {
            Thread newWindowThread = new Thread(new ThreadStart(() => {
                SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(Dispatcher.CurrentDispatcher));

                ChannelFrame window = new ChannelFrame(this);

                window.Closed += (s, e) => Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                windows.Add(name, window);
                callback(window);

                System.Windows.Threading.Dispatcher.Run();
            }));

            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.Start();
        }

        public ChannelFrame[] GetChannelFrames() {
            return (new List<ChannelFrame>(this.windows.Values)).ToArray();
        }

        public ChannelFrame GetChannelFrame(string name) {
            ChannelFrame window = null;

            if(!this.windows.TryGetValue(name, out window)) {
                return window;
            }

            return window;
        }

        public object GetCore() {
            return this;
        }

        public App GetAppCore() {
            return this.core;
        }

        public void setProxy(Proxy proxy) {
            this.proxy = proxy;
        }

        public void Start() {
            if(!CanConnect()) {
                MessageBox.Show("Can't connect to <" + this.hostname + ":" + this.port + ">.");
                return;
            }

            Connect();
            this.login.Show();
        }

        public bool CanConnect() {
            // @ToDo check Proxy is connectable?

            try {
                using(var client = new TcpClient(this.hostname, this.port)) {
                    client.Close();
                    return true;
                }
            } catch {
                return false;
            }
        }

        private void Connect() {
            // @ToDo Proxy connection

            this.socket = new TcpClient(this.hostname, this.port);
            Console.WriteLine("[Connect] " + this.hostname + ":" + this.port);

            new Thread(new ThreadStart(Run)).Start();
            
            Send(this.protocols.GetPacket("CONNECT").Get(), false, false);
            Send(((Klass.Packets.Handshake) this.protocols.GetPacket("HANDSHAKE")).Get(this.GetAppCore().GetConfig().GetAppletVersion()));
        }

        public Protocols GetProtocols() {
            return this.protocols;
        }

        private void Run() {
            Console.WriteLine("Running...");

            while(true) {

                try {
                    byte[] decoded      = Bundle.Decode(this.socket.GetStream(), this.key_connection);
                    Console.WriteLine("[RECEIVE] " + Encoding.UTF8.GetString(decoded, 0, decoded.Length));
                    string uncompressed = this.huffman.Decompress(decoded);
                    this.protocols.Handle(uncompressed);
                } catch (IOException e) {
                    Console.Write(e.Message);
                    break;
                }
            }
         }

        public void Send(byte[] data, bool encode, bool compress) {
            byte[] compressed;
            byte[] encoded;

            if(compress) {
                compressed = this.huffman.Compress(new BinaryBuffer().Append(data).ToString());
            } else {
                compressed = data;
            }

            if (encode) {
                encoded = Bundle.Encode(compressed);
            } else {
                encoded = compressed;
            }

            Console.WriteLine("[SEND] " + Encoding.UTF8.GetString(data, 0, data.Length));
            File.AppendAllText("log.txt", "[SEND] " + Encoding.UTF8.GetString(data, 0, data.Length) + "\n");
            this.socket.GetStream().Write(encoded, 0, encoded.Length);
        }

        public void Send(byte[] data) {
            Send(data, true, true);
        }

        public void SetPasswordHash(string value) {
            this.hash_password = value;
        }

        public string GetPasswordHash() {
            return this.hash_password;
        }

        public void SetSuggestion(string value) {
            this.login.SetSuggestion(value);
        }

        public void SetDecodeKey(string value) {
            this.key_connection = new UTF8Encoding().GetBytes(value);
        }

        public void SetChannelList(List<Channel> channels) {
            this.login.SetChannelList(channels);
        }
    }
}
