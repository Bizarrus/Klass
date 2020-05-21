/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using System;

namespace KnuCli.Protocol {
    class PublicMessage : Packet {
        public PublicMessage() {
            this.Name   = "e";
            this.ID     = "Public_MESSAGE";
        }

        public override void Handle(IClient client, Tokens tokens) {
            string nickname = tokens.GetString(1);
            string channel = tokens.GetString(2);
            string message = tokens.GetString(3);

            Client c = ((Client) client.GetCore());

            if (channel.Equals("-")) {
                foreach (UI.ChannelFrame window in c.GetChannelFrames()) {
                    window.AddMessage(UI.MessageType.PUBLIC, nickname, message);
                }
            } else {
                UI.ChannelFrame window = c.GetChannelFrame(channel);
                window.AddMessage(UI.MessageType.PUBLIC, nickname, message);
            }
        }
    }
}
