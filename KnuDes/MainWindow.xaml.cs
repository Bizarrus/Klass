using Klass.KCode;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace KnuDes {
	public partial class MainWindow : Window {
		private TextPanelLight kcode	= new TextPanelLight();
		private string placeholder		= "<Bitte hier den Text eingeben.>";
		private string preview          = "Hier wird der Text dann mit den _F°B°or°R°ma°Y°ti°W°er°O°un°r°gen_ angezeigt.";
		
		public MainWindow() {
			InitializeComponent();

			for(int size = 6; size < 100; size++) {
				this.DefaultFontSize.Items.Add(size);
			}

			this.DefaultFontSize.Text = "" + kcode.DefaultFontSize;
			((ColorPicker) this.Background).SelectedColor = Color.FromRgb(255, 255, 255);
			((ColorPicker) this.TextColor).SelectedColor = kcode.DefaultTextColor;
			((ColorPicker) this.LinkHover).SelectedColor = kcode.DefaultLinkHoverColor;
			((ColorPicker) this.Red).SelectedColor = kcode.ChannelRed;
			((ColorPicker) this.Green).SelectedColor = kcode.ChannelGreen;
			((ColorPicker) this.Blue).SelectedColor = kcode.ChannelBlue;
			this.Output.Children.Add(kcode);

			this.Input.Text			= placeholder;
			this.Input.MouseEnter	+= OnEnter;
			this.Input.MouseLeave	+= OnLeave;

			kcode.SetContent(this.preview);
		}

		private void OnChangeFontSize(object sender, EventArgs e) {
			kcode.DefaultFontSize = (int) this.DefaultFontSize.SelectedValue;
			kcode.Repaint();
		}

		private void OnChangeBackground(object sender, EventArgs e) {
			Color color = (Color) ColorConverter.ConvertFromString((string) ((ColorPicker) this.Background).SelectedColor.Value.ToString());

			((ColorPicker) this.Background).SelectedColor = color;
			this.Output.Background = new SolidColorBrush(color);
			kcode.Repaint();
		}

		private void OnChangeTextColor(object sender, EventArgs e) {
			Color color = (Color) ColorConverter.ConvertFromString((string) ((ColorPicker) this.TextColor).SelectedColor.Value.ToString());
			kcode.DefaultTextColor = color;
			((ColorPicker) this.TextColor).SelectedColor = color;
			kcode.Repaint();
		}

		private void OnChangeLinkHover(object sender, EventArgs e) {
			Color color = (Color) ColorConverter.ConvertFromString((string) ((ColorPicker) this.LinkHover).SelectedColor.Value.ToString());
			kcode.DefaultLinkHoverColor = color;
			((ColorPicker) this.LinkHover).SelectedColor = color;
			kcode.Repaint();
		}
		
		private void OnChangeRed(object sender, EventArgs e) {
			Color color = (Color) ColorConverter.ConvertFromString((string) ((ColorPicker) this.Red).SelectedColor.Value.ToString());
			kcode.ChannelRed = color;
			((ColorPicker) this.Red).SelectedColor = color;
			kcode.Repaint();
		}
		
		private void OnChangeGreen(object sender, EventArgs e) {
			Color color = (Color) ColorConverter.ConvertFromString((string) ((ColorPicker) this.Green).SelectedColor.Value.ToString());
			kcode.ChannelGreen = color;
			((ColorPicker) this.Green).SelectedColor = color;
			kcode.Repaint();
		}
		
		private void OnChangeBlue(object sender, EventArgs e) {
			Color color = (Color) ColorConverter.ConvertFromString((string) ((ColorPicker) this.Blue).SelectedColor.Value.ToString());
			kcode.ChannelBlue = color;
			((ColorPicker) this.Blue).SelectedColor = color;
			kcode.Repaint();
		}
		
		private void OnChangeAllowment(object sender, EventArgs e) {
			kcode.AllowBold = (bool) AllowBold.IsChecked;
			kcode.AllowItalic = (bool) AllowItalic.IsChecked;
			kcode.AllowFontSize = (bool) AllowFontSize.IsChecked;
			kcode.AllowColor = (bool) AllowColor.IsChecked;
			kcode.AllowImages = (bool) AllowImages.IsChecked;
			kcode.AllowLinks = (bool) AllowLinks.IsChecked;
			kcode.AllowAlignment = (bool) AllowAlignment.IsChecked;
			kcode.AllowIndentation = (bool) AllowIndentation.IsChecked;
			kcode.AllowBreaklines = (bool) AllowBreaklines.IsChecked;
			kcode.Repaint();
		}

		private void OnEnter(object sender, EventArgs e) {
			if(this.Input.Text.Equals(this.placeholder)) {
				this.Input.Text = "";
			}
		}

		private void OnLeave(object sender, EventArgs e) {
			if(this.Input.Text.Length == 0) {
				this.Input.Text = placeholder;
				kcode.SetContent(this.preview);
			}
		}

		private void OnChange(object sender, KeyEventArgs e) {
			Console.WriteLine("Change Text: " + this.Input.Text);
			kcode.SetContent(this.Input.Text);
		}
	}
}
