using QFramework;
using QuickLinker.Plugin.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickLinker.Systems
{
    public interface IFileIconSystem : ISystem
    {
        object GetImage(string filePath, int index);
    }
}
