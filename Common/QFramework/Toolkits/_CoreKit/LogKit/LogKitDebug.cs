using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace QFramework
{
    public class LogKitDebug : ILogKit
    {
        private static readonly ThreadLocal<StringBuilder> stringBuilder = new ThreadLocal<StringBuilder>();
        public void E(object msg, params object[] args)
        {
            if (stringBuilder.Value == null)
                stringBuilder.Value = new StringBuilder();
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            if (msg == null)
            {
                stringBuilder.Value.Append("[null]");
            }
            else
            {
                if (args == null || args.Length == 0)
                    stringBuilder.Value.Append(msg.ToString());
                else
                    stringBuilder.Value.AppendFormat(msg.ToString(), args);
            }
            string text = stringBuilder.ToString();
            Debug.WriteLine("[Eror] " + text);
        }

        public void E(Exception e)
        {
            if (stringBuilder.Value == null)
                stringBuilder.Value = new StringBuilder();
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            stringBuilder.Value.Append((e != null) ? e.ToString() : "[null]");
            string text = stringBuilder.ToString();
            Debug.WriteLine("[Eror] " + text);
        }

        public void I(object msg, params object[] args)
        {
            if (stringBuilder.Value == null)
                stringBuilder.Value = new StringBuilder();
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            if (msg == null)
            {
                stringBuilder.Value.Append("[null]");
            }
            else
            {
                if (args == null || args.Length == 0)
                    stringBuilder.Value.Append(msg.ToString());
                else
                    stringBuilder.Value.AppendFormat(msg.ToString(), args);
            }
            string text = stringBuilder.ToString();
            Debug.WriteLine("[logr] " + text);
        }

        public void W(object msg, params object[] args)
        {
            if (stringBuilder.Value == null)
                stringBuilder.Value = new StringBuilder();
            stringBuilder.Value.Clear();
            stringBuilder.Value.AppendFormat("{0:HH:mm:ss} ", DateTime.Now);
            if (msg == null)
            {
                stringBuilder.Value.Append("[null]");
            }
            else
            {
                if (args == null || args.Length == 0)
                    stringBuilder.Value.Append(msg.ToString());
                else
                    stringBuilder.Value.AppendFormat(msg.ToString(), args);
            }
            string text = stringBuilder.ToString();
            Debug.WriteLine("[Warn] " + text);
        }
    }
}
