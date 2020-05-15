/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
namespace KnuCli.Model {
    public class Proxy {
        public bool Enabled {
            get; set;
        }

        public int Port {
            get; set;
        }

        public string Hostname {
            get; set;
        }

        public override string ToString() {
            return "[Proxy Enabled=" + (this.Enabled ? "Yes" : "No") + ", Port=" + this.Port + ", Hostname=\"" + this.Hostname + "\"]";
        }
    }
}
