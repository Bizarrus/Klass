/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;

namespace Klass.Packets {
    public class Connect : Packet {
        public Connect() {
            this.ID     = "CONNECT";
            this.Name   = "0x00";
        }

        public override byte[] Get() {
            return new byte[] { 0x00 };
        }
    }
}
