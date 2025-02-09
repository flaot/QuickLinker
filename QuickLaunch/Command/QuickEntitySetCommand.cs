using QFramework;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.QuickLaunch.Utils;
using System.Drawing;
using System.IO;

namespace QuickLinker.QuickLaunch.Command
{
    public class QuickEntitySetCommand : AbstractCommand
    {
        public int index;
        public string filePath;
        public bool? adminStartUp;
        public string startArg;
        public string desc;
        public string workFolder;
        public Bitmap bitmapImage;
        public string actionHotKey;
        public bool? dropNLaunch;
        public bool? launchOnStartup;
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            var entity = entitySystem.Find(index);
            if (entity == null)
                return;
            bool change = false;
            if (filePath != null && !string.Equals(entity.Path, filePath))
            {
                change |= true;
                entity.Path = filePath;
            }
            if (adminStartUp.HasValue && adminStartUp.Value != entity.adminStartUp)
            {
                change |= true;
                entity.adminStartUp = adminStartUp.Value;
            }
            if (startArg != null && !string.Equals(entity.startArg, startArg))
            {
                change |= true;
                entity.startArg = startArg;
            }
            if (desc != null && !string.Equals(entity.desc, desc))
            {
                change |= true;
                entity.desc = desc;
            }
            if (workFolder != null && !string.Equals(entity.workFolder, workFolder))
            {
                change |= true;
                entity.workFolder = workFolder;
            }
            if (bitmapImage != null && !Equals(entity.bitmapImage, bitmapImage))
            {
                change |= true;
                entity.bitmapImage = bitmapImage;
                entity.imageByteArr = ImageUtil.BitmapImageToByte(bitmapImage);
            }
            if (actionHotKey != null && !string.Equals(entity.actionHotKey, actionHotKey))
            {
                change |= true;
                entity.actionHotKey = actionHotKey;
            }
            if (dropNLaunch.HasValue && dropNLaunch.Value != entity.dropNLaunch)
            {
                change |= true;
                entity.dropNLaunch = dropNLaunch.Value;
            }
            if (launchOnStartup.HasValue && launchOnStartup.Value != entity.launchOnStartup)
            {
                change |= true;
                entity.launchOnStartup = launchOnStartup.Value;
            }
            if (change)
            {
                entity.needSave.Value = change;
                entitySystem.ChangeEntityEvent.Trigger(index, entity);
            }
        }
    }
}
