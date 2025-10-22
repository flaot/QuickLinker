using System.Reflection;

namespace QuickLinker.Plugin.Menu
{
    /// <summary>
    /// 菜单对象的子项
    /// </summary>
    public class MenuItem
    {
        public string name;
        public Info info;
        public Action<Info> Func;

        public class Info : IComparable<Info>
        {
            /// <summary> 菜单路径 </summary>
            public string namePath;
            /// <summary> 菜单优先级 </summary>
            public int priority;
            /// <summary> 菜单类型 </summary>
            public int type;
            /// <summary> 静态方法的菜单是没有对象的 </summary>
            public object classObj;
            /// <summary> 菜单对应的执行方法 </summary>
            public MethodInfo MethodInfo;

            public int CompareTo(Info other)
            {
                var reault = priority.CompareTo(other.priority);
                if (reault != 0)
                    return reault;

                return namePath.CompareTo(other.namePath);
            }
            public override string ToString()
            {
                return namePath;
            }
        }
    }
}
