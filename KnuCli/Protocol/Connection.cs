/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using System;

namespace KnuCli.Protocol {
    class Connection : Packet {
        public Connection() {
            this.Name   = "(";
            this.ID     = "CONNECTION";
        }

        public override void Handle(IClient client, Tokens tokens) {
            ((Client) client).SetPasswordHash(tokens.GetString(1));
            ((Client) client).SetSuggestion(tokens.GetString(2));
            ((Client) client).SetDecodeKey(tokens.GetString(3));
        }
    }
}
