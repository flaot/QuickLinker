using System;

namespace QuickLinker.QuickLaunch.Models
{
    [Serializable]
    public class AppConfig
    {
        private bool followMouse = true;  //面板跟随鼠标 默认是
        public string SysBakTime;  //系统自动备份时间
        public string MenuPassword; //锁菜单密码

    }
}
