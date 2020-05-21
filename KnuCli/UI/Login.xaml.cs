/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using KnuCli.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace KnuCli.UI {
    public partial class Login : Window {
        private Client client;

        public Login(Client client) {
            this.client = client;
            InitializeComponent();

            if(this.client.GetAppCore().GetConfig().HasAutoLogin()) {
                Autologin autologin = this.client.GetAppCore().GetConfig().GetAutologin();
                this.Nickname.Text  = autologin.GetNickname();
                this.Password.Text  = autologin.GetPassword();
            }
        }

        private void Start(object sender, RoutedEventArgs e) {
            switch(this.client.GetAppCore().GetConfig().GetAppletVersion()) {
                case "k90cab":
                    string hash = Klass.Passwords.K90cab.Hash(this.Password.Text, this.client.GetPasswordHash());

                    SendLogin(this.Nickname.Text, hash, this.Channel.Text);
                break;
                default:
                    MessageBox.Show("Warning:\nThe Applet-Version " + this.client.GetAppCore().GetConfig().GetAppletVersion() + " has no Password-Hashing, trying the online-auth by Knuddels.");

                    Klass.Helper.Password.Check(this.Nickname.Text, this.Password.Text, delegate (JObject data) {
                        if((string) data.GetValue("error") != null) {
                            MessageBox.Show((string) data.GetValue("error"));
                            return;
                        }

                        if((JObject) data.GetValue("ok") != null) {
                            JObject ok          = (JObject) data.GetValue("ok");
                            string nickname     = (string) ok.GetValue("nick");
                            string password     = (string) ok.GetValue("pwd");
                            this.Nickname.Text  = nickname;

                            SendLogin(this.Nickname.Text, password, this.Channel.Text);
                            return;
                        }

                        MessageBox.Show("Unbekannter Login-Fehler aufgetreten!\n\nResult:\n" + data);
                    });
                    break;
            }
        }

        private void SendLogin(string nickname, string password, string channel) {
            this.client.Send(((Klass.Packets.Login) this.client.GetProtocols().GetPacket("LOGIN")).Get(nickname, password, channel));
        }

        public void SetSuggestion(string value) {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => {
                this.Channel.Text = value;
            }));
        }

        public void SetChannelList(List<Channel> channels) {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => {
                foreach(Channel channel in channels) {
                    ListBoxItem entry                   = new ListBoxItem();
                    StackPanel container                = new StackPanel();
                    container.Orientation               = Orientation.Horizontal;

                    Label label_name                    = new Label();
                    label_name.HorizontalAlignment      = HorizontalAlignment.Left;
                    label_name.VerticalAlignment        = VerticalAlignment.Center;
                    label_name.Content                  = channel.Name;
                    container.Children.Add(label_name);

                    Label label_online                  = new Label();
                    label_online.HorizontalAlignment    = HorizontalAlignment.Left;
                    label_online.VerticalAlignment      = VerticalAlignment.Center;
                    label_online.Content                = "(" + channel.OnlineUsers + ")";
                    label_online.Margin                 = new Thickness(-8, 0, -4, 0);
                    container.Children.Add(label_online);

                    /* Icons */
                    // <Image Source="https://chat.knuddels.de/pics/icon_fullChannel.gif" VerticalAlignment="Center" HorizontalAlignment="Left" />
                    entry.Content                       = container;
                    entry.Selected                      += OnChannelSelected;
                    entry.Tag                           = Convert.ToBase64String(new UTF8Encoding().GetBytes(channel.Name));

                    this.Channels.Items.Add(entry);
                }
            }));
        }

        private void OnChannelSelected(object sender, RoutedEventArgs e) {
            SetSuggestion(new UTF8Encoding().GetString(Convert.FromBase64String((string) (e.Source as ListBoxItem).Tag)));
        }
    }
}