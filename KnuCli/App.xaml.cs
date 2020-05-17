/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using KnuCli.UI;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KnuCli {
    public partial class App : Application {
        private Configuration configuration;
        private List<Client> clients;

        void Boot(object sender, StartupEventArgs e) {
            Console.WriteLine("Booting...");

            this.clients = new List<Client>();
            this.configuration = new Configuration();

            // @ToDo track closing-State
            new ClientCreation(this).Show();
        }

        public Configuration GetConfig() {
            return this.configuration;
        }

        public Client CreateClient(string hostname, int port) {
            Client client = new Client(this, hostname, port);

            this.clients.Add(client);

            return client;
        }
    }
}
