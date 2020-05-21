using System.Collections.Generic;
using System.Windows;

namespace Klass.KCode {
	public class KCodeElement {
		private string text                 = null;
		private List<int[]> bolds           = new List<int[]>();
		private List<int[]> italics         = new List<int[]>();
		private List<IExtended> extended    = new List<IExtended>();
		private TextAlignment alignment     = TextAlignment.Left;

		public string GetText() {
			return this.text;
		}

		public void SetText(string text) {
			this.text = text;
		}

		public List<int[]> GetBolds() {
			return this.bolds;
		}

		public void SetBold(List<int[]> bolds) {
			this.bolds = bolds;
		}

		public List<int[]> GetItalics() {
			return this.italics;
		}

		public void SetItalics(List<int[]> italics) {
			this.italics = italics;
		}

		public List<IExtended> GetExtended(Property type) {
			List<IExtended> result = new List<IExtended>();

			foreach(IExtended entry in this.extended) {
				if(entry.GetType().Equals(type)) {
					result.Add(entry);
				}
			}

			return result;
		}

		public void SetExtended(List<IExtended> extended) {
			this.extended = extended;
		}

		public void AddExtended(IExtended extended) {
			this.extended.Add(extended);
		}

		public TextAlignment GetAlignment() {
			return this.alignment;
		}

		public void SetAlignment(TextAlignment alignment) {
			this.alignment = alignment;
		}
	}
}
