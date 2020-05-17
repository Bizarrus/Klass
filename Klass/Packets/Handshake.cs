/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;

namespace Klass.Packets {
    public class Handshake : Packet {
        public Handshake() {
            this.ID     = "HANDSHAKE";
            this.Name   = "t";
        }

        public byte[] Get(string version) {
            BinaryBuffer buffer = new BinaryBuffer();

            // @ToDo variable settings
            buffer.Append(this.Name);
            buffer.AddNullByte();
            buffer.Append(version);                             // Client Version
            buffer.AddNullByte();
            buffer.Append("http://www.knuddels.de/");           // Referer / Location
            buffer.AddNullByte();
            buffer.Append("3");                                 // Category
            buffer.AddNullByte();
            buffer.Append("1.6.0_22");                          // Java Version
            buffer.AddNullByte();
            buffer.Append("-");                                 // ZIP Code
            buffer.AddNullByte();
            buffer.Append("46513");                             // Local Port
            buffer.AddNullByte();
            buffer.Append("Java HotSpot(TM) Server VM");        // Java VM Name
            buffer.AddNullByte();
            buffer.Append("-");                                 // Cookie

            return buffer.GetBytes();
        }
    }
}
