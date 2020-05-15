/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using KnuCli.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KnuCli.UI {
    public partial class Login : Window {
        private Client client;

        public Login(Client client) {
            this.client = client;
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e) {
            MessageBox.Show("Login..");
            
            //this.client.Send(Klass.Packets.Login.Get("Testnick", "testpw"));
        }

        public void SetSuggestion(string value) {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => {
                this.Channel.Text = value;
            }));
        }

        public void SetChannelList(List<Channel> channels) {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() => {
                foreach(Channel channel in channels) {
                    // @ToDo Styling, Databinding
                    this.Channels.Items.Add(channel.Name + " (" + channel.OnlineUsers + ")");
                }
            }));
        }
    }
}