using Klass.Helper;
using System;
using System.Windows;

namespace KnuCli.Protocol {
    class Popup : Packet {
        public Popup() {
            this.Name   = "k";
            this.ID     = "POPUP";
        }

        public override void Handle(IClient client, Tokens tokens) {
            MessageBox.Show(tokens.GetText());
        }
    }
}
