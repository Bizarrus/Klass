using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Klass.KCode {
	public enum Type {
		BOLD        = '_',
		ITALIC      = '"',
		EXTENDED    = '°',
		BREAK       = '#'
	}

	public enum Property {
		UNKNOWN = -1,
		IMAGE   = 0,
		LINK    = 1,
		COLOR   = 2,
		SIZE    = 4
	}

	public class KCodeParser {
		public bool AllowBold { get; set; } = true;
		public bool AllowItalic { get; set; } = true;
		public bool AllowFontSize { get; set; } = true;
		public bool AllowColor { get; set; } = true;
		public bool AllowImages { get; set; } = true;
		public bool AllowLinks { get; set; } = true;
		public bool AllowAlignment { get; set; } = true;
		public bool AllowIndentation { get; set; } = true;
		public bool AllowBreaklines { get; set; } = true;
		public int DefaultFontSize { get; set; } = 16;
		public Color DefaultTextColor { get; set; } = Color.FromRgb(0, 0, 0);
		public Color DefaultLinkHoverColor { get; set; } = Color.FromRgb(255, 0, 0);
		public Color ChannelRed { get; set; } = Color.FromRgb(255, 0, 255);
		public Color ChannelGreen { get; set; } = Color.FromRgb(0, 255, 0);
		public Color ChannelBlue { get; set; } = Color.FromRgb(0, 0, 255);

		private Color GetColor(char a, char b) {
			switch(a) {
				case 'R':
				return (b == 'R' ? this.ChannelRed : Color.FromRgb(255, 0, 0));
				case 'G':
				return (b == 'G' ? this.ChannelGreen : Color.FromRgb(0, 255, 0));
				case 'B':
				return (b == 'B' ? this.ChannelBlue : Color.FromRgb(0, 0, 255));
				case 'W':
				return Color.FromRgb(255, 255, 255);
				case 'K':
				return Color.FromRgb(0, 0, 0);
				case 'Y':
				return Color.FromRgb(255, 255, 0);
				case 'E':
				return Color.FromRgb(0, 172, 0);
				case 'N':
				return Color.FromRgb(150, 74, 0);
				case 'C':
				return Color.FromRgb(0, 255, 255);
				case 'D':
				return Color.FromRgb(64, 64, 64);
				case 'A':
				return Color.FromRgb(128, 128, 128);
				case 'O':
				return Color.FromRgb(255, 200, 0);
				case 'L':
				return Color.FromRgb(192, 192, 192);
				case 'P':
				return Color.FromRgb(255, 175, 175);
				case 'M':
				return Color.FromRgb(255, 0, 255);
				default:
				return Color.FromRgb(160, 100, 130);
			}
		}

		public List<KCodeElement> Parse(string content) {
			List<KCodeElement> result = new List<KCodeElement>();

			int position            = 0;
			int visual_position     = 0;
			char[] data             = content.ToCharArray();
			StringBuilder output    = new StringBuilder();
			KCodeElement entry      = new KCodeElement();

			bool is_bold            = false;
			int[] bold              = new int[] { -1, -1 };
			List<int[]> bolds       = new List<int[]>();

			bool is_italic          = false;
			int[] italic            = new int[] { -1, -1 };
			List<int[]> italics     = new List<int[]>();

			bool is_extended        = false;
			int[] extended          = new int[] { -1, -1 };
			StringBuilder extended_content  = new StringBuilder();

			for(; position < data.Length;) {
				++visual_position;
				char current = data[position++];

				switch(current) {
					case '\\':
						--visual_position;
						output.Append(data[position++]);
					break;
					case '\n':
						output.Append("\\n");
					break;
					case (char) Type.BREAK:
						if(this.AllowBreaklines) {
							output.Append("\n........ ");
						}
					break;
					case (char) Type.BOLD:
						if(this.AllowBold) {
							if(position < data.Length && data[position] == (char) Type.BOLD) {
								output.Append((char) Type.BOLD);
								++position;
								continue;
							}

							--visual_position;

							if(Array.IndexOf(data, (char) Type.BOLD, position - 1) > -1) {
								if(is_bold) {
									is_bold = false;
									bold[1] = output.ToString().Length;
									bolds.Add(bold);
									bold = new int[] { -1, -1 };
									continue;
								}
							}

							bold[0] = visual_position;
							is_bold = !is_bold;
						}
					break;
					case (char) Type.ITALIC:
						if(this.AllowItalic) {
							if(position < data.Length && data[position] == (char) Type.ITALIC) {
								output.Append((char) Type.ITALIC);
								++position;
								continue;
							}

							--visual_position;

							if(Array.IndexOf(data, (char) Type.ITALIC, position - 1) > -1) {
								if(is_italic) {
									is_italic = false;
									italic[1] = output.ToString().Length;
									italics.Add(italic);
									italic = new int[] { -1, -1 };
									continue;
								}
							}

							italic[0] = visual_position;
							is_italic = !is_italic;
						}
					break;
					case (char) Type.EXTENDED:
						if(position < data.Length && data[position] == (char) Type.EXTENDED) {
							output.Append((char) Type.EXTENDED);
							++position;
							continue;
						}

						--visual_position;

						if(Array.IndexOf(data, (char) Type.EXTENDED, position - 1) > -1) {
							if(is_extended) {
								is_extended = false;
								extended[1] = output.ToString().Length;
								output.Append(ParseExtended(entry, extended_content.ToString(), (position + 1 < data.Length ? data[position + 1] : '-'), extended));
								extended[1] = output.ToString().Length;
								extended_content = new StringBuilder();
								extended = new int[] { -1, -1 };
								continue;
							}
						}

						extended[0] = visual_position;
						is_extended = !is_extended;
					break;
					default:
						if(is_extended) {
							extended_content.Append(current);
						} else {
							output.Append(current);
						}
					break;
				}
			}

			entry.SetText(output.ToString());
			entry.SetBold(bolds);
			entry.SetItalics(italics);
			result.Add(entry);

			return result;
		}

		private string ParseExtended(KCodeElement element, string data, char next, int[] position) {
			/* Image or Link */
			if(data.StartsWith(">") && data.EndsWith("<")) {
				string url = data.Substring(1, data.Length - 2);

				if(url.EndsWith(".gif") || url.EndsWith(".png") || url.EndsWith(".jpg") || url.EndsWith(".jpeg")) {
					Console.WriteLine("\tImage: " + url);
					Extending.Image style = new Extending.Image();
					style.SetPosition(position);
					style.SetValue((object) url);
					element.AddExtended(style);
					return null; //"<PIC:" + url + "~" + position[0] + ">";

				} else {
					//position[1] = ending;
					Console.WriteLine("\tLink: " + url);
					Extending.Link style = new Extending.Link();
					style.SetPosition(position);
					style.SetValue((object) url);
					element.AddExtended(style);

					if(url.Contains("|")) {
						string[] parts = url.Split('|');
						style.SetText(parts[0]);
						return parts[0];
					}

					style.SetText("Link");

					return "Link";
				}

			/* Font Size */
			} else if(data.All(char.IsDigit)) {
				Extending.FontSize style = new Extending.FontSize();
				style.SetPosition(position);

				try {
					style.SetValue((object) Int32.Parse(data));
					element.AddExtended(style);
				} catch(Exception e) {
					Console.WriteLine("Error: " + e.Message);
				}

			/* RGB Color */
			} else if(data.StartsWith("[") && data.EndsWith("]")) {
				string rgb      = data.Substring(1, data.Length - 2);
				string[] parts  = rgb.Split(',');
				try {
					Color color     = Color.FromRgb((byte) Int32.Parse(parts[0]), (byte) Int32.Parse(parts[1]), (byte) Int32.Parse(parts[2]));

					Extending.Color style = new Extending.Color();
					style.SetPosition(position);
					style.SetValue((object) color);
					element.AddExtended(style);
				} catch(Exception e) {
					Console.WriteLine("Error: " + e.Message);
				}

			/* Restore */
			} else if(data.Equals("r")) {
				Extending.FontSize size = new Extending.FontSize();
				size.SetPosition(position);
				size.SetValue((object) this.DefaultFontSize);
				element.AddExtended(size);

				Extending.Color style = new Extending.Color();
				style.SetPosition(position);
				style.SetValue((object) this.DefaultTextColor);
				element.AddExtended(style);

			/* Color */
			} else {
				Color color     = GetColor((char) data[0], next);
				Extending.Color style = new Extending.Color();
				style.SetPosition(position);
				style.SetValue((object) color);
				element.AddExtended(style);
			}

			return null;
		}
	}
}
