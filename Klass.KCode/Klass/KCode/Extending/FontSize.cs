namespace Klass.KCode.Extending {
    public class FontSize : Property, IExtended {
        public new KCode.Property GetType() {
            return KCode.Property.SIZE;
        }
    }
}
