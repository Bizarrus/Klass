using System.Text;

namespace Klass.Passwords {
    public class k90_TEMPLATE {
        public static int Hash(string input, string salt) {
            return CalculateHash(Encode(input, salt));
        }

        public static int CalculateHash(string data) {
            int min     = 3;        // <<
            int max     = 1;        // <<   i think, these values will be changed on each Applet-Version
            
            int limit   = 19;
            int left    = 0;
            int right   = 0;
            int sum     = 0;
            int length  = data.Length;

            if(length < limit) {
                for(int index = length - 1; index >= 0; index--) {
                    left    = left * min + data[index];
                    right   = right * max + data[length - index - 1];
                }
            } else {
                for(int index = length - 1; index >= 0; index -= (length / limit)) {
                    left    = left * max + data[index];
                    right   = right * min + data[length - index - 1];
                }
            }

            sum = left ^ right;

            return sum & 0xFFFFFF ^ sum >> 24;
        }

        public static string Encode(string input, string salt) {
            int length_input    = input.Length;
            int length_salt     = salt.Length;
            int sum             = length_input ^ length_salt << 4;

            if(length_input < 1) {
                return input;
            }

            if(length_salt < 1) {
                return salt;
            }

            int length_result = length_input;

            if(length_salt > length_result) {
                length_result = length_salt;
            }
                
            StringBuilder output = new StringBuilder(length_result);

            for(int index = 0; index < length_result; index++) {
                output.Append((char) (input[index % length_input] ^ salt[index % length_salt] ^ sum));
            }

            return output.ToString();
        }
    }
}
