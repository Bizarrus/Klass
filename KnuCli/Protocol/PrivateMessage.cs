/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using System;

namespace KnuCli.Protocol {
    class PrivateMessage : Packet {
        public PrivateMessage() {
            this.Name   = "r";
            this.ID     = "PRIVATE_MESSAGE";
        }

        public override void Handle(IClient client, Tokens tokens) {
            string sender = tokens.GetString(1);
            string receiver = tokens.GetString(2);
            string channel = tokens.GetString(3);
            string message = tokens.GetString(4);
            string from_channel = tokens.GetString(5);

            Client c = ((Client) client.GetCore());

            if (channel.Equals("-")) {
                foreach (UI.ChannelFrame window in c.GetChannelFrames()) {
                    window.AddMessage(UI.MessageType.PRIVATE, sender, message);
                }
            } else {
                UI.ChannelFrame window = c.GetChannelFrame(channel);
                window.AddMessage(UI.MessageType.PRIVATE, sender, message);
            }
        }
    }
}
