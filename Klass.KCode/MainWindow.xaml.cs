using System;
using System.Windows;

namespace Klass.KCode {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Console.WriteLine("New Window");
            AddKCode("Simpler Test in _Fett_ und \"kursiv\".");
            AddKCode("Wir testen mal __das Escaping \\_Haha");
            AddKCode("_O_der _e_infach _a_lle _e_rsten _B_uchstaben.");
            AddKCode("Jetzt testen wir °R°einfache°r° Farbe.");
            AddKCode("Oder als °[255,0,255]°RGB°r° Wert");
            AddKCode("Hier ein einfacher °>https://google.de/<°");
            AddKCode("Hier kommst du nach °>Google|https://google.de/<°");
            AddKCode("Oder mal ein Bild: °>https://chat.knuddels.de/pics/gt.gif<°");
            AddKCode("Ändern wir mal die °30°Fontsize°r°, du Opfer.");
            AddKCode("Mal eine \nNeue Line.");
            AddKCode("KCode#Neue Line.");
        }

        public void AddKCode(string content) {
            Console.WriteLine("Add Content: " + content);
            TextPanelLight panel = new TextPanelLight(content);
            panel.AllowBold = true;
            panel.AllowItalic = true;
            this.Output.Children.Add(panel);
        }
    }
}
