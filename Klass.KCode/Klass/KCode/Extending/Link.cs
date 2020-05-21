namespace Klass.KCode.Extending {
	class Link : Property, IExtended {
		private string text = "Link";

		public new KCode.Property GetType() {
			return KCode.Property.LINK;
		}

		public string GetText() {
			return this.text;
		}

		public void SetText(string text) {
			this.text = text;
		}
	}
}
