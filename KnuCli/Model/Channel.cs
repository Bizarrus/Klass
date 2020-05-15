/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System;
using System.Collections.Generic;

namespace KnuCli.Model {
    public class Channel {
        public string Name {
            get; set;
        }

        public bool Children {
            get; set;
        }

        public int OnlineUsers {
            get; set;
        }

        public string A {
            get; set;
        }
        
        public string B {
            get; set;
        }

        public List<string> Icons {
            get; set;
        }

        public override string ToString() {
            return "[Channel Name=\"" + this.Name + "\", OnlineUsers=" + OnlineUsers +  ", Children=" + Children + ", A=" + A + ", B=" + B + ", Icons=(" + String.Join(",", Icons) + ")]";
        }
    }
}
