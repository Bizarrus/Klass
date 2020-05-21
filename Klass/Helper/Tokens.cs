/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System;

namespace Klass.Helper {
    public class Point {
        private int x = 0;
        private int y = 0;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public int GetX() {
            return this.x;
        }

        public int GetY() {
            return this.y;
        }

        public string ToString() {
            return "[Point X=" + this.x + ", Y=" + this.y + "]";
        }
    }
    
    public class Dimension {
        private int width   = 0;
        private int height  = 0;

        public Dimension(int width, int height) {
            this.width  = width;
            this.height = height;
        }

        public int GetWidth() {
            return this.width;
        }

        public int GetHeight() {
            return this.height;
        }

        public string ToString() {
            return "[Dimension Width=" + this.width + ", Height=" + this.height + "]";
        }
    }

    public class Color {
        private int red     = -1;
        private int green   = -1;
        private int blue    = -1;

        public Color(int red, int green, int blue) {
            this.red    = red;
            this.green  = green;
            this.blue   = blue;
        }

        public int GetRed() {
            return this.red;
        }

        public int GetGreen() {
            return this.green;
        }

        public int GetBlue() {
            return this.blue;
        }

        public string GetRGB() {
            return this.red + "," + this.green + "," + this.blue;
        }

        public string GetHEX() {
            return string.Format("{0:X2}{1:X2}{2:X2}", this.red, this.green, this.blue).ToUpper();
        }

        public string ToString() {
            return "[Color Red=" + this.red + ", Green=" + this.green + ", Blue=" + this.blue + ", RGB=" + this.GetRGB() + ", HEX=" + this.GetHEX() + "]";
        }
    }

    public class Bool {
        private bool value = false;

        public Bool(bool value) {
            this.value = value;
        }
        public bool GetValue() {
            return this.value;
        }

        public string ToString() {
            return "[Bool Value=" + (this.value ? "True" : "False") + "]";
        }
    }

    public class Tokens {
        private string[] tokens;

        public Tokens(string data) {
            this.tokens = data.Split('\0');
        }

        public string GetText() {
            return String.Join("\\0", this.tokens);
        }

        public string GetString(int index) {
            try {
                return this.tokens[index];
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public int GetInteger(int index) {
            try {
                return Int32.Parse(this.tokens[index]);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        public Bool GetBoolean(int index) {
            string value    = this.GetString(index).ToUpper();

            if(value.Equals("T") || value.Equals("1") || value.Equals("TRUE")) {
                return new Bool(true);
            }

            return new Bool(false);
        }
        
        public Color GetColor(int index) {
            string value    = this.GetString(index);

            if(value.StartsWith("[")) {
                value = value.Trim('[');
            }

            if(value.EndsWith("]")) {
                value = value.Trim(']');
            }

            string[] values = value.Split(',');

            int red     = Int32.Parse(values[0]);
            int green   = Int32.Parse(values[1]);
            int blue    = Int32.Parse(values[2]);

            return new Color(red, green, blue);
        }

        public Point GetPoint(int index) {
            return new Point(this.GetInteger(index), this.GetInteger(index + 1));
        }

        public Dimension GetDimension(int index) {
            return new Dimension(this.GetInteger(index), this.GetInteger(index + 1));
        }

        public int Size() {
            return this.tokens.Length;
        }

        public int GetEnum(int index) {
            return this.GetInteger(index);
        }
    }
}
