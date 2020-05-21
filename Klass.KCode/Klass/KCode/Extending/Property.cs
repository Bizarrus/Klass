namespace Klass.KCode.Extending {
	public abstract class Property {
		private object value    = null;
		private int[] position  = new int[] { -1, -1 };

		public int[] GetPosition() {
			return this.position;
		}

		public void SetPosition(int[] position) {
			this.position = position;
		}

		public object GetValue() {
			return this.value;
		}

		public void SetValue(object value) {
			this.value = value;
		}
	}
}
