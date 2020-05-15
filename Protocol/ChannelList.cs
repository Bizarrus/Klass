/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using KnuCli.Model;
using System;
using System.Collections.Generic;

namespace KnuCli.Protocol {
    public class ChannelList : Packet {
        public List<Channel> channels;

        public ChannelList() {
            this.Name       = "b";
            this.ID         = "CHANNEL_LIST";
            this.channels   = new List<Channel>();
        }

        public override void Handle(IClient client, Tokens tokens) {
            this.channels.Clear();

            int subchannel = 0;
            string previous = null;

            for (int index = 1; index < tokens.Size(); index++) {
                string[] channel    = tokens.GetString(index++).Split('\n');
                string name         = channel[0];
                int online          = 0;

                if(channel.Length == 2) {
                    online = Int16.Parse(channel[1]);
                }

                if(name.Trim().Length == 0) {
                    break;
                }

                if(name == "\"") {
                    name = previous + " " + (++subchannel);
                } else if(name.StartsWith("\"")) {
                    name = previous + " " + name.Substring(1);
                } else {
                    subchannel  = 0;
                    previous    = name;
                }

                channels.Add(new Channel {
                    Name        = name,
                    Children    = name.StartsWith("\""),
                    OnlineUsers = online,
                    A           = tokens.GetString(index++),     // p(lain), i(talic), B(old)?
                    B           = tokens.GetString(index++),     // K
                    Icons       = this.GetIcons(tokens, ref index)
                });
            }

            ((Client) client).SetChannelList(channels);
        }

        private List<string> GetIcons(Tokens tokens, ref int index) {
            List<string> icons = new List<string>();
            
            for(; index < tokens.Size();) {
                string icon = tokens.GetString(index);

                if (icon == "-") {
                    break;
                }

                icons.Add(icon);
                ++index;
            }

            return icons;
        }
    }
}
