/**
 * @Author     Bizzi
 * @Version    1.0.0
 */

using KnuCli.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace KnuCli {
    public class Autologin {
        private string nickname = null;
        private string password = null;

        public Autologin(string nickname, string password) {
            this.nickname = nickname;
            this.password = password;
        }

        public string GetNickname() {
            return this.nickname;
        }

        public string GetPassword() {
            return this.password;
        }
    }

    public class Configuration {
        public List<ChatSystem> systems = new List<ChatSystem>();
        private string user_agent = "";
        private string applet_version = "";
        private Autologin autologin = null;
        private List<string> modules;
        private Proxy proxy;

        public Configuration() {
            this.modules = new List<string>();
            LoadConfiguration();
            LoadServers();
        }

        private void LoadConfiguration() {
            // @ToDo bad path - Only for DEV!
            using(JsonTextReader reader = new JsonTextReader(File.OpenText(@"../../Config/Configuration.json"))) {
                JObject options         = (JObject) JToken.ReadFrom(reader);
                JObject proxy_settings  = (JObject) options.GetValue("proxy");
                this.user_agent         = (string) options.GetValue("user_agent");
                this.applet_version     = (string) options.GetValue("applet_version");
                this.proxy              = new Proxy();
                this.proxy.Enabled      = (bool) proxy_settings.GetValue("enabled");
                this.proxy.Hostname     = (string) proxy_settings.GetValue("hostname");
                this.proxy.Port         = (int) proxy_settings.GetValue("port");

                foreach (string entry in (JArray) options.GetValue("modules")) {
                    this.modules.Add(entry);
                }

                Console.WriteLine(String.Join("\n", this.modules));
            }
        }

        private void LoadServers() {
            // @ToDo bad path - Only for DEV!
            using(JsonTextReader reader = new JsonTextReader(File.OpenText(@"../../Config/Servers.json"))) {
                foreach(JObject entry in (JArray) JToken.ReadFrom(reader)) {
                    this.systems.Add(new ChatSystem {
                        ID          = (string) entry.GetValue("id"),
                        Port        = (int) entry.GetValue("port"),
                        Hostname    = (string) entry.GetValue("hostname")
                    });
                }
            }

            Console.WriteLine(String.Join("\n", this.systems));
        }

        public string GetUserAgent() {
            return this.user_agent;
        }
        
        public string GetAppletVersion() {
            return this.applet_version;
        }
        
        public Proxy GetProxy() {
            return this.proxy;
        }

        public List<ChatSystem> GetChatSystems() {
            return this.systems;
        }

        public ChatSystem GetChatSystem(int index) {
            return this.systems[index];
        }

        public void SetAutologin(string nickname, string password) {
            this.autologin = new Autologin(nickname, password);
        }

        public bool HasAutoLogin() {
            return (this.autologin != null);
        }

        public Autologin GetAutologin() {
            return this.autologin;
        }
    }
}
