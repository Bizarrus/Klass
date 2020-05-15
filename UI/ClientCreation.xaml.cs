/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using KnuCli.Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KnuCli.UI {
    public partial class ClientCreation : Window {
        private bool handle_chatsystem = true;
        private App core;
        private bool huffman = true; // @ToDo by settings

        public ClientCreation(App core) {
            this.core = core;

            InitializeComponent();

            foreach(ChatSystem entry in this.core.GetConfig().GetChatSystems()) {
                this.ChatSystem.Items.Add(entry.ID + " - " + entry.Hostname + ":" + entry.Port);
            }

            this.ChatSystem.Items.Add("<Manual>");

            this.UserAgent.Text         = this.core.GetConfig().GetUserAgent();
            this.AppletVersion.Text     = this.core.GetConfig().GetAppletVersion();
            this.ProxyEnabled.IsChecked = this.core.GetConfig().GetProxy().Enabled;
            this.ProxyHostname.Text     = this.core.GetConfig().GetProxy().Hostname;
            this.ProxyPort.Text         = "" + this.core.GetConfig().GetProxy().Port;
        }

        private void ChatSystemClosed(object sender, EventArgs e) {
            if(this.handle_chatsystem) {
                ChatSystemHandle();
            }

            this.handle_chatsystem = true;
        }

        private void ChatSystemSelected(object sender, SelectionChangedEventArgs e) {
            this.handle_chatsystem  = !(sender as ComboBox).IsDropDownOpen;
            ChatSystemHandle();
        }

        private void ChatSystemHandle() {
            if(this.ChatSystem.SelectedItem == null) {
                return;
            }

            if(this.ChatSystem.SelectedItem.ToString() == "<Manual>") {
                this.Hostname.Text      = "";
                this.Port.Text          = "";
                this.Hostname.IsEnabled = true;
                this.Port.IsEnabled     = true;
            } else {
                ChatSystem entry        = this.core.GetConfig().GetChatSystem(this.ChatSystem.SelectedIndex);
                this.Hostname.Text      = entry.Hostname;
                this.Port.Text          = "" + entry.Port;
                this.Hostname.IsEnabled = false;
                this.Port.IsEnabled     = false;
            }
        }

        private void ProxyCheck(object sender, RoutedEventArgs e) {
            if(((CheckBox) sender).IsChecked ?? false) {
                this.ProxyHostname.IsEnabled    = true;
                this.ProxyPort.IsEnabled        = true;
            } else {
                this.ProxyHostname.IsEnabled    = false;
                this.ProxyPort.IsEnabled        = false;
            }
        }
        private void HuffmanCheck(object sender, RoutedEventArgs e) {
            this.huffman = (((CheckBox) sender).IsChecked ?? false);
        }

        private void Start(object sender, RoutedEventArgs e) {
            if(this.Hostname.Text.Trim().Length == 0) {
                MessageBox.Show("Please enter an Hostname!");
                return;
            }

            if(this.Port.Text.Trim().Length == 0) {
                MessageBox.Show("Please enter an valid Port!");
                return;
            }

            Client client = this.core.CreateClient(this.Hostname.Text.Trim(), Int16.Parse(this.Port.Text.Trim()));

            if(this.ProxyEnabled.IsChecked ?? false) {
                client.setProxy(new Proxy {
                    Enabled     = true,
                    Hostname    = this.ProxyHostname.Text,
                    Port        = Int16.Parse(this.ProxyPort.Text)
                });
            }

            client.Start();
        }
	}
}
