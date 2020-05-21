/**
 * @Author     Bizzi
 * @Version    1.0.0
 */
using Klass.Helper;
using KnuCli.UI;
using System;

namespace KnuCli.Protocol {
    class ChannelFrame : Packet {
        public ChannelFrame() {
            this.Name   = "a";
            this.ID     = "CHANNEL_FRAME";
        }

        public override void Handle(IClient client, Tokens tokens) {
            string name                         = tokens.GetString(1);
            string nickname                     = tokens.GetString(2);
            int scrollspeed                     = tokens.GetInteger(3);
            Dimension size                      = tokens.GetDimension(4);
            Point position                      = tokens.GetPoint(6);
            string background_image             = tokens.GetString(8);
            BackgroundStyle background_style    = (BackgroundStyle) tokens.GetEnum(9);
            string unknown                      = tokens.GetString(10);
            Color foreground                    = tokens.GetColor(11);
            Color background                    = tokens.GetColor(12);
            Color channel_red                   = tokens.GetColor(13);
            Color channel_blue                  = tokens.GetColor(14);
            int font_size                       = tokens.GetInteger(15);
            int line_height                     = tokens.GetInteger(16);
            int nicklist_font_size              = tokens.GetInteger(17);
            Color nicklist_background           = tokens.GetColor(18);
            Bool unknown2                       = tokens.GetBoolean(19);
            string combobox_value               = tokens.GetString(20);
            Bool combobox_visible               = tokens.GetBoolean(21);
            int message_maximum                 = tokens.GetInteger(22);
            Bool button_help                    = tokens.GetBoolean(23);
            Bool button_report                  = tokens.GetBoolean(24);
            Bool button_feedback                = tokens.GetBoolean(25);
            Bool button_search                  = tokens.GetBoolean(26);
            string unknown3                     = tokens.GetString(27);
            string unknown4                     = tokens.GetString(28);
            Bool unknown5                       = tokens.GetBoolean(29);

            Console.WriteLine("INCOMING CHANNEL FRAME");
            Console.WriteLine("\tname = " + name);
            Console.WriteLine("\tnickname = " + nickname);
            Console.WriteLine("\tscrollspeed = " + scrollspeed);
            Console.WriteLine("\tsize = " + size.ToString());
            Console.WriteLine("\tposition = " + position.ToString());
            Console.WriteLine("\tbackground_image = " + background_image);
            Console.WriteLine("\tbackground_style = " + background_style);
            Console.WriteLine("\tunknown = " + unknown);
            Console.WriteLine("\tforeground = " + foreground.ToString());
            Console.WriteLine("\tbackground = " + background.ToString());
            Console.WriteLine("\tchannel_red = " + channel_red.ToString());
            Console.WriteLine("\tchannel_blue = " + channel_blue.ToString());
            Console.WriteLine("\tfont_size = " + font_size);
            Console.WriteLine("\tline_height = " + line_height);
            Console.WriteLine("\tnicklist_font_size = " + nicklist_font_size);
            Console.WriteLine("\tnicklist_background = " + nicklist_background.ToString());
            Console.WriteLine("\tunknown2 = " + unknown2.ToString());
            Console.WriteLine("\tcombobox_value = " + combobox_value);
            Console.WriteLine("\tcombobox_visible = " + combobox_visible.ToString());
            Console.WriteLine("\tmessage_maximum = " + message_maximum);
            Console.WriteLine("\tbutton_help = " + button_help.ToString());
            Console.WriteLine("\tbutton_report = " + button_report.ToString());
            Console.WriteLine("\tbutton_feedback = " + button_feedback.ToString());
            Console.WriteLine("\tbutton_search = " + button_search.ToString());
            Console.WriteLine("\tunknown3 = " + unknown3);
            Console.WriteLine("\tunknown4 = " + unknown4);
            Console.WriteLine("\tunknown5 = " + unknown5.ToString());

             ((Client) client.GetCore()).CreateChannelFrame(name, delegate(UI.ChannelFrame window) {
                 window.SetChannel(name);
                 window.SetNickname(nickname);
                 window.SetSize(size);
                 window.SetLocation(position);
                 window.SetScrollspeed(scrollspeed);
                 window.SetForeground(foreground);
                 window.SetBackground(background);
                 window.SetBackground(background_image, background_style);
                 window.SetChannelStyle(channel_red, channel_blue);
                 window.SetFontSize(font_size);
                 window.SetLineHeight(line_height);
                 window.SetNicklist(nicklist_font_size, nicklist_background);

                 window.Show();
             });
        }
    }
}
