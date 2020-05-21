namespace Klass.KCode.Extending {
	public class Color : Property, IExtended {
		public new KCode.Property GetType() {
			return KCode.Property.COLOR;
		}
	}
}
