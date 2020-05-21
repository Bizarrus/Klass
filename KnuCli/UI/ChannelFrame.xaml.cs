using Klass.Helper;
using Klass.KCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KnuCli.UI {
    public enum MessageType {
        PUBLIC = 0,
        PRIVATE = 1,
        ACTION = 2
    }

    public enum BackgroundStyle {
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        I = 9,
        J = 10,
        K = 11,
        L = 12,
        M = 13,
        N = 14,
        O = 15,
        P = 16,
        Q = 17,
        R = 18,
        S = 19,
        T = 20,
        U = 21,
        V = 22,
        W = 23,
        X = 24,
        Y = 25,
        Z = 26

    }

    public partial class ChannelFrame : Window {
        private Client client = null;
        private string name = null;
        private string nickname = null;
        private Dimension dimension = null;
        private Klass.Helper.Point position = null;
        private int scrollspeed = -1;
        private Klass.Helper.Color foreground = null;
        private Klass.Helper.Color background = null;
        private Klass.Helper.Color channel_red = null;
        private Klass.Helper.Color channel_blue = null;

        public ChannelFrame(Client client) {
            this.client = client;

            InitializeComponent();
        }

        public void SetChannel(string name) {
            this.name = name;
        }

        public void SetNickname(string nickname) {
            this.nickname = nickname;
        }

        public void SetSize(Dimension dimension) {
            this.dimension = dimension;
        }

        public void SetLocation(Klass.Helper.Point position) {
            this.position = position;
        }

        public void SetScrollspeed(int scrollspeed) {
            this.scrollspeed = scrollspeed;
        }

        public void SetForeground(Klass.Helper.Color foreground) {
            this.foreground = foreground;
        }

        public void SetBackground(Klass.Helper.Color background) {
            this.background = background;
        }

        public void SetChannelStyle(Klass.Helper.Color channel_red, Klass.Helper.Color channel_blue) {
            this.channel_red = channel_red;
            this.channel_blue = channel_blue;
        }

        public void SetLineHeight(int line_height) {
            
        }

        public void SetFontSize(int font_size) {
            
        }

        public void SetNicklist(int font_size, Klass.Helper.Color background) {
           
        }

        public void SetBackground(string background_image, BackgroundStyle background_style) {
           
        }

        public void AddMessage(MessageType type, string sender, string message) {
            Dispatcher.Invoke(() => {
                Console.WriteLine("Add Message: " + message);
                TextPanelLight panel = new TextPanelLight();
                panel.AllowBold = true;
                panel.AllowItalic = true;

                switch (type) {
                    case MessageType.PUBLIC:
                        panel.SetContent("_" + sender + ":_ " + message);
                    break;
                    case MessageType.PRIVATE:
                        panel.SetContent("°BB°_" + sender  + "_°r°" + message + "°r°");
                    break;
                    case MessageType.ACTION:
                        panel.SetContent("°[" + this.channel_blue.GetRGB()  + "]°" + message + "°r°");
                    break;
                }

                this.Output.Children.Add(panel);
            });
        }

        public new void Show() {
            base.Show();
            this.Title = "Channel: " + this.name + ", Nickname: " + this.nickname;
            this.Width = this.dimension.GetWidth();
            this.Height = this.dimension.GetHeight();
            this.Top = this.position.GetX();
            this.Left = this.position.GetY();
        }
    }
}
