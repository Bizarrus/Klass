namespace Klass.KCode.Extending {
    class Link : Property, IExtended {
        public new KCode.Property GetType() {
            return KCode.Property.LINK;
        }
    }
}
