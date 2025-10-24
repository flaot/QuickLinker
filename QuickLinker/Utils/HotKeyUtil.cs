using System;
using System.Text;
using System.Windows.Forms;
using WK.Libraries.HotkeyListenerNS;

namespace QuickLinker.Utils
{
    internal class HotKeyUtil
    {
        public static void Control_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as Control;
            if (e.KeyCode == Keys.Tab)
                return;
            if (e.KeyCode == Keys.Escape)
            {
                textBox.Text = string.Empty;
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
            textBox.Text = Convert(e);
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        public static void Control_KeyUp(object sender, KeyEventArgs e)
        {
            var textBox = sender as Control;
            string str = textBox.Text.Trim();
            if (str.Length >= 1 && str.Substring(str.Length - 1) == "+")
            {
                textBox.Text = string.Empty;
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                textBox.Text = string.Empty;
                return;
            }
        }
        public static string Convert(KeyEventArgs e)
        {
            StringBuilder keyValue = new StringBuilder();
            if (e.Modifiers != 0)
            {
                if (e.Control)
                    keyValue.Append("Ctrl + ");
                if (e.Alt)
                    keyValue.Append("Alt + ");
                if (e.Shift)
                    keyValue.Append("Shift + ");
            }

            keyValue.Append(e.KeyCode.ToString());
            return keyValue.ToString();
        }
        public static Hotkey Convert(string hotKeyStr)
        {
            Hotkey hotkey = new Hotkey();
            if (hotKeyStr.Contains("Ctrl + "))
                hotkey.Modifiers |= Keys.Control;
            if (hotKeyStr.Contains("Alt + "))
                hotkey.Modifiers |= Keys.Alt;
            if (hotKeyStr.Contains("Shift + "))
                hotkey.Modifiers |= Keys.Shift;
            if (hotKeyStr.Contains('+'))
                hotKeyStr = hotKeyStr.Substring(hotKeyStr.LastIndexOf('+') + 1).Trim();

            hotkey.KeyCode = (Keys)Enum.Parse(typeof(Keys), hotKeyStr, ignoreCase: true);
            return hotkey;
        }
    }
}
