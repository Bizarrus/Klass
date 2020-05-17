/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using System.IO;
using System.Threading;

namespace KnuPro {
    public class Debugger {
        static ReaderWriterLock Locker = new ReaderWriterLock();

        public static void Write(string String) {
            try {
                Locker.AcquireWriterLock(int.MaxValue);
                File.AppendAllText("log.txt", String);
            } finally {
                Locker.ReleaseWriterLock();
            }
        }

        public static void Clear() {
            try {
                Locker.AcquireWriterLock(int.MaxValue);
                File.WriteAllText("log.txt", "");
            } finally {
                Locker.ReleaseWriterLock();
            }
        }
    }
}
