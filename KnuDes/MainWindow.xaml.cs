using Klass.KCode;
using System;
using System.Windows;
using System.Windows.Input;

namespace KnuDes {
	public partial class MainWindow : Window {
		private TextPanelLight kcode = new TextPanelLight();

		public MainWindow() {
			InitializeComponent();
			this.Output.Children.Add(kcode);
		}

		private void OnChange(object sender, KeyEventArgs e) {
			Console.WriteLine("Change Text: " + this.Input.Text);
			kcode.SetContent(this.Input.Text);
		}
	}
}
