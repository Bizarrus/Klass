using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Klass.KCode {
    public class TextPanelLight : Canvas {
        private string content = null;
        private KCodeParser parser = null;
        private List<KCodeElement> elements = new List<KCodeElement>();
        public bool AllowBold { get; set; } = true;
        public bool AllowItalic { get; set; } = true;
        public bool AllowFontSize { get; set; } = true;
        public bool AllowColor { get; set; } = true;
        public bool AllowImages { get; set; } = true;
        public bool AllowLinks { get; set; } = true;
        public bool AllowAlignment { get; set; } = true;
        public bool AllowIndentation { get; set; } = true;
        public bool AllowBreaklines { get; set; } = true;
        public int DefaultFontSize { get; set; } = 16;
        public Color DefaultTextColor { get; set; } = Color.FromRgb(0, 0, 0);
        public Color DefaultLinkHoverColor { get; set; } = Color.FromRgb(255, 0, 0);
        public Color ChannelRed { get; set; } = Color.FromRgb(255, 0, 0);
        public Color ChannelGreen { get; set; } = Color.FromRgb(0, 255, 0);
        public Color ChannelBlue { get; set; } = Color.FromRgb(0, 0, 255);

        public TextPanelLight() {
            base.BeginInit();
            Console.WriteLine("New TextPanelLight");
            this.parser = new KCodeParser();
        }

        public TextPanelLight(string content) {
            base.BeginInit();
            Console.WriteLine("New TextPanelLight");

            this.content = content;
            this.parser = new KCodeParser();
            this.elements = this.parser.Parse(this.content);
        }

        public void SetContent(string content) {
            this.content = content;
            this.elements = this.parser.Parse(this.content);
        }

        protected override void OnRender(DrawingContext graphics) {
            base.OnRender(graphics);

            parser.AllowBold = this.AllowBold;
            parser.AllowItalic = this.AllowItalic;
            parser.AllowFontSize = this.AllowFontSize;
            parser.AllowColor = this.AllowColor;
            parser.AllowImages = this.AllowImages;
            parser.AllowLinks = this.AllowLinks;
            parser.AllowAlignment = this.AllowAlignment;
            parser.AllowIndentation = this.AllowIndentation;
            parser.AllowBreaklines = this.AllowBreaklines;
            parser.DefaultFontSize = this.DefaultFontSize;
            parser.DefaultTextColor = this.DefaultTextColor;
            parser.DefaultLinkHoverColor = this.DefaultLinkHoverColor;
            parser.ChannelRed = this.ChannelRed;
            parser.ChannelGreen = this.ChannelGreen;
            parser.ChannelBlue = this.ChannelBlue;
            string font_name = "Arial";

            foreach (KCodeElement entry in this.elements) {
                FormattedText element = new FormattedText(entry.GetText(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(font_name), parser.DefaultFontSize, new SolidColorBrush(parser.DefaultTextColor), VisualTreeHelper.GetDpi(this).PixelsPerDip);

                if (this.AllowBold) {
                    foreach (int[] bold in entry.GetBolds()) {
                        try {
                            element.SetFontWeight(FontWeights.Bold, bold[0], bold[1] - bold[0]);
                        } catch (Exception) {
                            Console.WriteLine("TextPanelLight: Bad position for Bold: " + bold[0] + ":" + (bold[1] - bold[0]));
                        }
                    }
                }

                if (this.AllowItalic) {
                    foreach (int[] italic in entry.GetItalics()) {
                        try {
                            element.SetFontStyle(FontStyles.Italic, italic[0], italic[1] - italic[0]);
                        } catch (Exception) {
                            Console.WriteLine("TextPanelLight: Bad position for Italic: " + italic[0] + ":" + (italic[1] - italic[0]));
                        }
                    }
                }

                if (this.AllowFontSize) {
                    foreach (IExtended property in entry.GetExtended(Property.SIZE)) {
                        int[] position = property.GetPosition();
                        int end = entry.GetText().Length - position[0] - 4;

                        if(position[1] > -1) {
                            end = position[0] - position[1];
                        }

                        try {
                            element.SetFontSize((int) property.GetValue(), position[0], end);
                        } catch (Exception) {
                            Console.WriteLine("TextPanelLight: Bad position for FontSize: " + position[0] + ":" + end);
                        }
                    }
                }

                if (this.AllowColor) {
                    foreach (IExtended property in entry.GetExtended(Property.COLOR)) {
                        int[] position = property.GetPosition();
                        int end = entry.GetText().Length - position[0] - 4;
                        Color color = (Color) property.GetValue();

                        if (position[1] > -1) {
                            end = position[0] - position[1];
                        }

                        if (color.Equals(Color.FromRgb(160, 100, 130))) {
                            color = parser.DefaultTextColor;
                        }

                        try {
                            element.SetForegroundBrush(new SolidColorBrush(color), position[0], end);
                        } catch (Exception) {
                            Console.WriteLine("TextPanelLight: Bad position for Color: " + position[0] + ":" + end);
                        }
                    }
                }

                if(this.AllowImages) {
                    foreach(IExtended property in entry.GetExtended(Property.IMAGE)) {
                        int[] position  = property.GetPosition();
                        string url      = (string) property.GetValue();

                        try {
                            BitmapImage img = new BitmapImage(new Uri(url));
                            graphics.DrawImage(img, new Rect(position[0], (position[1] - position[0]), img.PixelWidth, img.PixelHeight));
                        } catch (Exception) {
                            Console.WriteLine("TextPanelLight: Bad position for Image: " + position[0] + ":" + (position[1] - position[0]));
                        }
                    }
                }

                if(this.AllowLinks) {
                    foreach (IExtended property in entry.GetExtended(Property.LINK)) {
                        int[] position  = property.GetPosition();
                        string url      = (string) property.GetValue();

                        try {
                            element.SetTextDecorations(TextDecorations.Underline, position[0], url.Length);
                            element.SetForegroundBrush(new SolidColorBrush(Color.FromRgb(0, 0, 255)), position[0], url.Length);

                        } catch (Exception) {
                            Console.WriteLine("TextPanelLight: Bad position for Link: " + position[0] + ":" + position[1] + ":" + url.Length);
                        }
                    }
                }

                if(this.AllowAlignment) {
                    // element.TextAlignment = entry.GetAlignment();
                }

                if(this.AllowIndentation) {

                }

                graphics.DrawText(element, new Point(0, 0));
                this.Height = element.Height;
                //this.Width  = element.Width - (element.MaxTextWidth / 2);
            }
        }
    }
}
