/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;

namespace KnuCli.Protocol {
    class Ping : Packet {
        public Ping() {
            this.Name = ",";
            this.ID = "PING";
        }

        public byte[] Get(string id) {
            BinaryBuffer buffer = new BinaryBuffer();

            buffer.Append("h");
            buffer.AddNullByte();

            if(id != null) {
                buffer.Append(id);
            }

            return buffer.GetBytes();
        }

        public override void Handle(IClient client, Tokens tokens) {
            client.Send(Get(tokens.GetString(1)));
        }
    }
}
