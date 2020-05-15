/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Klass.Helper {
    public class BinaryBuffer {
        private List<byte> buffer;

        public BinaryBuffer() {
            this.buffer = new List<byte>();
        }

        public BinaryBuffer Clear() {
            this.buffer.Clear();

            return this;
        }

        public BinaryBuffer AddNullByte() {
            this.buffer.Add((byte) 0x00);

            return this;
        }

        public int Size() {
            return this.buffer.Count;
        }

        public BinaryBuffer Remove(int from, int to) {
            for(int index = from; index <= to; index++) {
                this.buffer.RemoveAt(index);
            }

            return this;
        }

        public BinaryBuffer Append(char data) {
            return Append("" + data);
        }
        
        public BinaryBuffer Append(string data) {
            return Append(new UTF8Encoding().GetBytes(data));
        }
        
        public BinaryBuffer Append(int data) {
            return Append(BitConverter.GetBytes(data));
        }

        public BinaryBuffer Append(byte data) {
            this.buffer.Add(data);

            return this;
        }
        
        public BinaryBuffer Append(byte[] data) {
            foreach(byte entry in data) {
                this.buffer.Add(entry);
            }

            return this;
        }

        public override string ToString() {
            return new UTF8Encoding().GetString(GetBytes());
        }

        public byte[] GetBytes() {
            return this.buffer.ToArray();
        }
    }
}
