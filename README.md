# Klass
Knuddels Toolset for external connections

- Contains standard methods for connections to Knuddels
- Various protocol files

# KnuCli
Knuddels-Client based on Klass

> This Client can be used for Emulators like Banana-Chat!

![Start](https://raw.githubusercontent.com/Bizarrus/Klass/master/Screenshots/Start.png)
![Login](https://raw.githubusercontent.com/Bizarrus/Klass/master/Screenshots/Login.png)

# KnuPro
Proxy based on Klass

> You must change your Host files to map the subdomain `chat.knuddels.de` to `127.0.0.1`

# Klass.KCode
Full implemented KCode parser in `C#`

> **WARNING:** The parser is currently under development!

```c#
TextPanelLight panel = new TextPanelLight(<content>);

/* Allow some Snippets */
parser.AllowBold				= true;
parser.AllowItalic				= true;
parser.AllowFontSize			= true;
parser.AllowColor				= true;
parser.AllowImages				= true;
parser.AllowLinks				= true;
parser.AllowAlignment			= true;
parser.AllowIndentation			= true;
parser.AllowBreaklines			= true;

/* Default Styles */
parser.DefaultFontSize			= 16;
parser.DefaultTextColor			= Color.FromRgb(0, 0, 0);
parser.DefaultLinkHoverColor	= Color.FromRgb(255, 0, 0);
parser.ChannelRed				= Color.FromRgb(255, 0, 0);
parser.ChannelGreen				= Color.FromRgb(0, 255, 0);
parser.ChannelBlue				= Color.FromRgb(0, 0, 255);

/* You can set the content on constructor or here */
panel.SetContent(<content>);

/* Add the TextPanelLight to your UI */
this.Output.Children.Add(panel);
```

![KCode](https://raw.githubusercontent.com/Bizarrus/Klass/master/Screenshots/KCode.png)