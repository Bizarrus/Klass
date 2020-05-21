namespace Klass.KCode.Extending {
    class Image : Property, IExtended {
        public new KCode.Property GetType() {
            return KCode.Property.IMAGE;
        }
    }
}
