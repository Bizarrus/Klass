/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using KnuCli.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KnuCli {
    public partial class App : Application {
        private Configuration configuration;
        private List<Client> clients;


        void Boot(object sender, StartupEventArgs e) {
            string hash = Klass.Passwords.K90cab.Hash("MeinPasswort", "knuddels");
            
            Console.WriteLine("Booting...");
                       
            this.clients = new List<Client>();
            this.configuration = new Configuration();

            string nickname = null;
            string password = null;

            for(int index= 0; index != e.Args.Length; ++index) {
                if(e.Args[index].StartsWith("--nickname=")) {
                    nickname = e.Args[index].Replace("--nickname=", "");
                } else if (e.Args[index].StartsWith("--password=")) {
                    password = e.Args[index].Replace("--password=", "");
                }
            }

            this.configuration.SetAutologin(nickname, password);

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
