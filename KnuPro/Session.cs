/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Networking;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace KnuPro {
    class Utils {
        public static string StringToHex(string input) {
            StringBuilder output = new StringBuilder();

            foreach(char character in input) {
                output.Append(Convert.ToInt32(character).ToString("x") + " ");
            }

            return output.ToString();
        }

        public static int CountToken(string Data) {
            return Data.Split('\0').Length;
        }

        public static string GetOpcode(string Data) {
            return GetToken(Data, 0);
        }

        public static string GetToken(string Data, int index) {
            return Data.Split('\0')[index];
        }

        public static string LogPacket(byte[] Data) {
            return LogPacket(new UTF8Encoding().GetString(Data));
        }

        public static string LogPacket(string Data) {
            Data = Data.Replace("\0", "\\0");
            Data = Data.Replace("\n", "\\n");
            return Data;
        }
    }

    abstract class Threading {
        private Thread Thread;

        protected Threading() {
            this.Thread = new Thread(new ThreadStart(this.Run));
        }

        public void Start() => this.Thread.Start();

        public void Join() => this.Thread.Join();

        public bool IsAlive => this.Thread.IsAlive;

        public abstract void Run();
    }

    class Server : Threading {
        private Session Session;
        private Socket Socket;
        private IPEndPoint Destination;
        private string PacketKey = null;

        public Server(Session Session, IPEndPoint Destination) {
            Console.WriteLine("Create the Server-Client...");
            this.Session        = Session;
            this.Destination    = Destination;
            this.Socket         = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Connect();
        }

        public override void Run() {
            try {
                while(true) {
                    int Availale = this.Socket.Available;

                    if(Availale > 0) {
                        Console.WriteLine("[Server > Client] " + Availale + " bytes");

                        byte[] Buffer = new byte[Availale];

                        this.Socket.Receive(Buffer);


                        Console.WriteLine("Forward the Data to Client...");
                        this.Session.Send(Buffer);

                        byte[] Decoded  = Bundle.Decode(Buffer, GetPacketKey());
                        string Data     = new Huffman().Decompress(Decoded);

                        switch(Utils.GetOpcode(Data)) {
                            case "(":
                                this.PacketKey = Utils.GetToken(Data, 1);
                                Console.WriteLine("[Info] Received PacketKey: " + this.PacketKey);
                            break;
                        }

                        Debugger.Write("[Server > Client (" + Utils.GetOpcode(Data) + ")] " + Utils.LogPacket(Data) + "\n");
                        Console.WriteLine("\t" + Data + "\n");
                    }
                }
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        public void Send(byte[] Data) {
            if(!this.Socket.Connected) {
                Console.WriteLine("Error: No Server-Connection!");
                Disconnect();
                return;
            }

            this.Socket.Send(Data);
        }

        public byte[] GetPacketKey() {
            if(this.PacketKey == null) {
                return null;
            }

            return new UTF8Encoding().GetBytes(this.PacketKey);
        }

        public void Connect() {
            Console.WriteLine("Connect to the Server...");


            this.Socket.Connect(this.Destination);
        }

        public void Disconnect() {
            Console.WriteLine("Disconnect the Server...");
            this.Socket.Close();
        }
    }

    class Session : Threading {
        private Server ChatServer   = null;
        private Socket ChatClient   = null;

        public Session(IPEndPoint Destination, Socket Socket) {
            Console.WriteLine("Create the Client...");

            this.ChatClient = Socket;
            this.ChatServer = new Server(this, Destination);
        }

        public override void Run() {
            this.ChatServer.Start();

            try {
                while(true) {
                    int Availale    = this.ChatClient.Available;

                    if(Availale > 0) {
                        byte[] Buffer   = new byte[Availale];

                        Console.WriteLine("[Client > Server] " + Availale + " bytes");

                        this.ChatClient.Receive(Buffer);

                        Console.WriteLine("Forward the Data to Server...");
                        this.ChatServer.Send(Buffer);

                        byte[] Decoded  = Bundle.Decode(Buffer, this.ChatServer.GetPacketKey());
                        string Data     = new Huffman().Decompress(Decoded);

                        Debugger.Write("[Client > Server (" + Utils.GetOpcode(Data) + ")] " + Utils.LogPacket(Decoded) + "\n");
                        Console.WriteLine("\t" + Data + "\n");

                        int size;

                        switch(Utils.GetOpcode(Data)) {
                            case "t":
                                Debugger.Write("!!!!!!!!!!!!!!!!!!!!!!!!!!!! HANDSHAKE !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                size = Utils.CountToken(Data);
                                Debugger.Write("Size: " + size);

                                for(int index = 0; index < size; index++) {
                                    string token = Utils.GetToken(Data, index);

                                    Debugger.Write("Token " + index +": " + Utils.StringToHex(token)  + " (" + token + ")\n");
                                }

                                Debugger.Write("!!!!!!!!!!!!!!!!!!!!!!!!!!!! HANDSHAKE !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                Environment.Exit(0);
                            break;
                            case "n":
                                Debugger.Write("!!!!!!!!!!!!!!!!!!!!!!!!!!!! LOGIN !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                size = Utils.CountToken(Data);
                                Debugger.Write("Size: " + size);

                                for(int index = 0; index < size; index++) {
                                    string token = Utils.GetToken(Data, index);

                                    Debugger.Write("Token " + index +": " + Utils.StringToHex(token)  + " (" + token + ")\n");
                                }

                                Debugger.Write("!!!!!!!!!!!!!!!!!!!!!!!!!!!! LOGIN !!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                                Environment.Exit(0);
                            break;
                        }
                    }
                }
            } catch(Exception e) {
                Console.WriteLine(e);
                this.ChatServer.Disconnect();
                this.ChatClient.Disconnect(false);
            }
        }

        public void Send(byte[] Data) {
            if(!this.ChatClient.Connected) {
                Console.WriteLine("Error: No Client-Connection!");
                return;
            }

            this.ChatClient.Send(Data);
        }

        
    }
}
