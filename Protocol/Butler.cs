/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using System;

namespace KnuCli.Protocol {
    class Butler : Packet {
        public Butler() {
            this.Name   = "5";
            this.ID     = "BUTLER";
        }

        public override void Handle(IClient client, Tokens tokens) {
            Console.WriteLine("Hey, Butler (" + tokens.GetString(1) + ")");
        }
    }
}
