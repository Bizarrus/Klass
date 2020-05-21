/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
namespace Klass.Helper {
    public interface IClient {
        object GetCore();
        void Send(byte[] data, bool encode, bool compress);
        void Send(byte[] data);
    }

    public class Packet {
        public virtual string ID {
            get; set;
        }

        public virtual string Name {
            get; set;
        }

        public virtual byte[] Get() {
            /* Override Me */
            return null;
        }

        public virtual void Handle(IClient client, Tokens tokens) {
            /* Override Me */
        }
    }
}
