/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System;

namespace Klass.Helper {
    public class Tokens {
        private string[] tokens;

        public Tokens(string data) {
            this.tokens = data.Split('\0');
        }

        public string GetString(int index) {
            try {
                return this.tokens[index];
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int Size() {
            return this.tokens.Length;
        }
    }
}
