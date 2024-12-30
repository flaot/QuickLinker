using System;

namespace QFramework
{
    public interface ILogKit
    {
        void I(object msg, params object[] args);
        void W(object msg, params object[] args);
        void E(object msg, params object[] args);
        void E(Exception e);
    }
}
