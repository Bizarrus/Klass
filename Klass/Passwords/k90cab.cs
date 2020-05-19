/**
 * @Author		SeBi, Bizzi
 * @Thanks		Brainy
 * @Version		1.0.0
 */
using System;
using System.Text;

namespace Klass.Passwords {
	public static class K90cab {
		public static string Hash(String password, string key) {
			key				= XOR(key, "vkUEOGHMFeh");

			int salt_length	= key.Length;
			int magic		= salt_length / 2;

			salt_length		= (magic * magic) + magic + 12;
			salt_length		*= salt_length % key.Length + 37;
			magic			= salt_length % key.Length;
			salt_length		= magic * magic;
			salt_length		= (salt_length + magic + 14) % key.Length;
			salt_length		= (key[salt_length] + 2) % key.Length;

			key				= string.Format("{0}{1}", key[salt_length], XOR(key, "RB"));
			key				= XOR(String.Format("{0}", (CalcInt(key) ^ 1640034174)), key);

			return "0~" + Escape(HashWithKey(password, key));
		}
		
		// Ab hier bleibt eigentlich immer alles gleich | Extrahiert aus 90bzy
		private static string Escape(string input) {
			var output = new StringBuilder();

			for(int index = 0; index < input.Length; ++index) {
				char character = input[index];

				if(character == 0) {
					output.Append("\u00010");
				} else if(character == 1) {
					output.Append("\u0001\u0001");
				} else {
					output.Append(character);
				}
			}

			return output.ToString();
		}

		private static string XOR(string input, string salt) {
			int magic				= salt.Length ^ input.Length << 3;
			int size				= salt.Length > input.Length ? salt.Length : input.Length;
			var output				= new StringBuilder(size);
			int position_input		= input[0];
			int position_salt		= salt[0];

			for(int index = 0; index < size; index++) {
				if(position_input >= salt.Length) {
					position_input = 0;
				}

				if(position_salt >= input.Length) {
					position_salt = 0;
				}

				output.Append((char) (input[position_salt] ^ salt[position_input] ^ magic));

				++position_input;
				++position_salt;
			}

			return output.ToString();
		}

		private static int Hash(string input) {
			int minimum = 0;
			int maximum = 0;
			int length = input.Length;
			int calculated_hash;

			if(length < 19) {
				for(calculated_hash = length - 1; calculated_hash >= 0; calculated_hash--) {
					minimum	= minimum * 3 + input[calculated_hash];
					maximum	= maximum * 5 + input[length - calculated_hash - 1];
				}
			} else {
				calculated_hash	= length / 19;
				int position	= length - 1;

				while(position >= 0) {
					minimum		= minimum * 5 + input[position];
					maximum		= maximum * 3 + input[length - position - 1];
					position	-= calculated_hash;
				}
			}

			calculated_hash = minimum ^ maximum;

			return calculated_hash & 0xFFFFFF ^ calculated_hash >> 24;
		}
		private static string HashWithKey(string input, string key) {
			var output	= new StringBuilder();
			int magic	= Hash(key);

			for(int j = 0; j < input.Length; j++) {
				magic = NextRandom(magic);

				output.Append((char) (input[j] ^ magic & 127));
			}

			return output.ToString();
		}

		private static int NextRandom(int input) {
			uint num	= (uint) input;
			uint i		= (num + 7) * 3;
			num			= num >> 8 | (uint) num << 24;
			uint j		= (uint) num >> 7 & 0x3FF;
			uint k		= (uint) num >> 22 & 0x3FF;
			num			^= (uint) (j * k + 5 * (i + 3));

			return (int) num;
		}

		private static int CalcInt(string input) {
			int i = 0;
			int j = 0;
			int k = 0;

			for(int m = 0; m < input.Length; m++) {
				j = j * ((k & 0x3) != 3 ? 5 : 3) + input[input.Length - m - 1];
				k = i ^ j;
				i = i * ((k & 0x4) == 0 ? 3 : 7) + input[m * 43973 % input.Length];
			}

			return k >> 26 ^ (j ^ i) & 0x3FFFFFF;
		}
	}
}