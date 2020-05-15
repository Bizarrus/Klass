/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;

namespace Klass.Packets {
    public class Login : Packet {
        public Login() {
            this.ID     = "LOGIN";
            this.Name   = "n";
        }

        public byte[] Get(string nickname, string password) {
            BinaryBuffer buffer = new BinaryBuffer();

            buffer.Append(this.Name);
            buffer.AddNullByte();
            buffer.Append(nickname);
            buffer.AddNullByte();
            buffer.Append(password);

            return buffer.GetBytes();
        }
    }
}
