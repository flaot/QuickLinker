using QuickLinker.Plugin.Menu;
using QuickLinker.Properties;

namespace QuickLinker.Menus
{
    public class MenuKey
    {
        public const string AppMenu_Show = nameof(AppMenu_Show);
        public const string AppMenu_ShowSetting = nameof(AppMenu_ShowSetting);
        public const string AppMenu_Quit = nameof(AppMenu_Quit);
                
        public const string FolderMenu_Copy = nameof(FolderMenu_Copy);
        public const string FolderMenu_Move = nameof(FolderMenu_Move);
        public const string FolderMenu_Delate = nameof(FolderMenu_Delate);
        public const string FolderMenu_Cancel = nameof(FolderMenu_Cancel);
                
        public const string PageMenu_Null = nameof(PageMenu_Null);
        public const string PageMenu_CreateQuick = nameof(PageMenu_CreateQuick);
        public const string PageMenu_SystemContext = nameof(PageMenu_SystemContext);
        public const string PageMenu_Copy = nameof(PageMenu_Copy);
        public const string PageMenu_Switch = nameof(PageMenu_Switch);
        public const string PageMenu_Align = nameof(PageMenu_Align);
        public const string PageMenu_Clear = nameof(PageMenu_Clear);
        public const string PageMenu_Attr = nameof(PageMenu_Attr);
                
        public const string TabMenu_MoveLeft = nameof(TabMenu_MoveLeft);
        public const string TabMenu_MoveRight = nameof(TabMenu_MoveRight);
        public const string TabMenu_Rename = nameof(TabMenu_Rename);
        public const string TabMenu_Delete = nameof(TabMenu_Delete);
        public const string TabMenu_Stand = nameof(TabMenu_Stand);
        public const string TabMenu_Button = nameof(TabMenu_Button);
        public const string TabMenu_Flot = nameof(TabMenu_Flot);
                
        public const string ToolStatusMenu_Time = nameof(ToolStatusMenu_Time);
        public const string ToolStatusMenu_Date = nameof(ToolStatusMenu_Date);
        public const string ToolStatusMenu_DateTime = nameof(ToolStatusMenu_DateTime);
        public const string ToolStatusMenu_None = nameof(ToolStatusMenu_None);
    }
    internal class MenuDefaultLang : IMenuLang
    {
        public int Priority => 0;
        public string GetLang(string menuPathKey)
        {
            var displayPath = Resources.ResourceManager.GetString(menuPathKey);
            if (string.IsNullOrEmpty(displayPath))
                return string.Empty;
            return displayPath;
        }
    }
}
