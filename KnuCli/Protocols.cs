/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
 
using Klass.Helper;
using Klass.Packets;
using KnuCli.Protocol;
using System;
using System.Collections.Generic;
using System.IO;

namespace KnuCli {
    public class Protocols {
        private Client client;
        private List<Packet> protocols;

        public Protocols(Client client) {
            this.protocols  = new List<Packet>();
            this.client     = client;

            // @ToDo autoload Load Files
            this.protocols.Add(new Connect());
            this.protocols.Add(new Handshake());
            this.protocols.Add(new Login());

            this.protocols.Add(new Butler());
            this.protocols.Add(new Connection());
            this.protocols.Add(new ChannelList());
            this.protocols.Add(new Popup());
            this.protocols.Add(new Generic());
            this.protocols.Add(new PublicMessage());
            this.protocols.Add(new PrivateMessage());
            this.protocols.Add(new ChannelFrame());

            /*
             * @ToDo
             * +
             */
        }

        public void Handle(string data) {
            File.AppendAllText("log.txt", "[RECEIVE] " + data + "\n");
            Console.WriteLine("[RECEIVE] " + data);

            Tokens tokens   = new Tokens(data);
            Packet packet   = GetPacket(tokens.GetString(0));

            if(packet == null) {
                Console.WriteLine("Can't found Packet with Opcode or ID \"" + tokens.GetString(0)  + "\".");
                return;
            }

            packet.Handle(client, tokens);
        }

        public Packet GetPacket(string opcode) {
            Packet found = null;

            foreach (Packet entry in this.protocols) {
                if(found != null) {
                    break;
                }

                if(entry.ID == opcode || entry.Name == opcode) {
                    found = entry;
                    break;
                }
            }

            if(found == null) {
                return null;
            }

            return found;
        }
    }
}
