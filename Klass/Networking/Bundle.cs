/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System.IO;
using System.Net.Sockets;

namespace Klass.Networking {
    public class Bundle {
        public static byte[] Encode(byte[] data) {
            byte[] header;
            int length = data.Length - 1;

            if (length < 128) {
                header = new byte[] {
                    (byte) length
                };
            } else {
                int size = 0;

                while(32 << (size + 1 << 3) <= length) {
                    size++;
                }

                header      = new byte[(size++) + 1];
                header[0]   = (byte) (size << 5 | 0x80 | length & 0x1F);

                for(int index = 1; index < header.Length; index++) {
                    header[index] = (byte) (length >> 8 * (index - 1) + 5);
                }
            }

            byte[] packet = new byte[header.Length + data.Length];
            header.CopyTo(packet, 0);
            data.CopyTo(packet, header.Length);
            return packet;
        }

        public static byte[] Decode(NetworkStream stream) {
            return Decode(stream, null);
        }

        public static byte[] Decode(NetworkStream stream, byte[] secret) {
            int size;
            sbyte header = (sbyte) stream.ReadByte();

            if(header == -1) {
                throw new IOException("End of stream");
            }

            if(header >= 0) {
                size        = header + 1;
            } else {
                size        = (header & 0x1F) + 1;

                for(int index = 0; index < ((header & 0x60) >> 5); index++) {
                    size += (stream.ReadByte() << (index << 3) + 5);
                }
            }

            byte[] package = new byte[size];

            for(int index = 0; index < size; index++) {
                package[index] = (byte) ((byte) stream.ReadByte() ^ (secret != null && index < secret.Length ? secret[index] : 0));
            }

            return package;
        }
    }
}
