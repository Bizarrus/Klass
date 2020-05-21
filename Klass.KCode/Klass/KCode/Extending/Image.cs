using System;
using System.Windows.Media.Imaging;

namespace Klass.KCode.Extending {
	class Image : Property, IExtended {
		public new KCode.Property GetType() {
			return KCode.Property.IMAGE;
		}

		public new void SetValue(object value) {
			base.SetValue(new BitmapImage(new Uri((string) value)));
		}

		public BitmapImage GetImage() {
			return (BitmapImage) GetValue();
		}
	}
}
