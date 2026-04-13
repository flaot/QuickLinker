using QuickLinker.QuickLaunch.Constant;
using QuickLinker.QuickLaunch.Models;

namespace QuickLinker.QuickLaunch.Utils
{
    /// <summary> 与主界面 MainForm_LoadButtonMissingFile 提示条件对齐的 UI 判定。 </summary>
    public static class EntityLaunchUi
    {
        /// <summary> 左键启动：与 MainForm 中 OpenType.OTHER 且无法解析目标 的分支一致。 </summary>
        public static bool BlocksLaunchClick(Entity entity, IProcessUtil processUtil) =>
            entity != null && processUtil != null
            && entity.iconType == OpenType.OTHER
            && !processUtil.CanResolveLaunchTarget(entity);

        /// <summary> Ctrl+左键打开目录：与 MainForm 中无法定位资源管理器路径 的分支一致。 </summary>
        public static bool BlocksExplorerClick(Entity entity, IProcessUtil processUtil) =>
            entity != null && processUtil != null && !processUtil.CanOpenInExplorer(entity);

        /// <summary> 格点红叉：任一路径会弹出「找不到文件」时显示。 </summary>
        public static bool ShowsLoadButtonMissingFileWarning(Entity entity, IProcessUtil processUtil) =>
            BlocksLaunchClick(entity, processUtil) || BlocksExplorerClick(entity, processUtil);
    }
}
