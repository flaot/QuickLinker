using QFramework;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.QuickLaunch.Utils;
using System.Drawing;

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
        protected override void OnExecute()
        {
            var entitySystem = this.GetSystem<QuickEntitySystem>();
            var entity = entitySystem.Find(index);
            if (entity == null)
                return;
            bool change = false;
            if (filePath != null && !string.Equals(entity.path, filePath))
            {
                change |= true;
                entity.path = filePath;
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
            if (change)
            {
                entity.needSave.Value = change;
                entitySystem.ChangeEntityEvent.Trigger(index, entity);
            }
        }
    }
}
