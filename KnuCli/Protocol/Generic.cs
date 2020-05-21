/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using System;

namespace KnuCli.Protocol {
    class Generic : Packet {
        public Generic() {
            this.Name   = ":";
            this.ID     = "GENERIC_PROTOCOL";
        }

        public override void Handle(IClient client, Tokens tokens) {
            string packet = tokens.GetText();

            if(packet.StartsWith(this.Name + "\0")) {
                packet = packet.Substring(2);
            }

            Console.WriteLine("INCOMING GENERIC PROTOCOL");
            Console.WriteLine("\t " + packet);
        }
    }
}
