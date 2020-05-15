/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
namespace KnuCli.Model {
    public class ChatSystem {
        public string ID {
            get; set;
        }
        
        public int Port {
            get; set;
        }
        
        public string Hostname {
            get; set;
        }

        public override string ToString() {
            return "[ChatSystem ID=\"" + this.ID + "\", Port=" + this.Port + ", Hostname=\"" + this.Hostname + "\"]";
        }
    }
}