/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using System.Text.RegularExpressions;

namespace Klass.Packets {
    public class Handshake : Packet {
        public Handshake() {
            this.ID     = "HANDSHAKE";
            this.Name   = "t";
        }

        private string CreateVersion(string version) {
            Regex regex = new Regex(@"k([0-9]+)([a-z]+)");
            Match match = regex.Match(version);

            if(match.Success) {
                string number   = match.Groups[1].Value;
                string name     = match.Groups[2].Value;

                if(!number.Contains(".")) {
                    char[] values   = number.ToCharArray();
                    number          = new string(new char[] {
                        values[0],
                        '.',
                        values[1]
                    });
                }

                return "V" + number + name + " ";
            }

            return version + " ";
        }

        public byte[] Get(string version) {
            BinaryBuffer buffer = new BinaryBuffer();

            // @ToDo variable settings
            buffer.Append(this.Name);
            buffer.AddNullByte();
            // k90cab
            buffer.Append(CreateVersion(version));              // Client Version
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
