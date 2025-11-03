using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLinker.QuickLaunch.Utils
{
    public interface IURIUtil : IUtility
    {
        string Protocol { get; }
        void Set(bool enable);
    }
}
