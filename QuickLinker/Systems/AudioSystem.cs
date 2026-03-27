using QFramework;
using QuickLinker.Model;
using QuickLinker.Properties;
using System.IO;
using System.Media;

namespace QuickLinker.Systems
{
    public enum AudioType
    {
        None = 0,
        Click,
        Group,
        Drop,
        Button,
    }

    public class AudioInfo
    {
        /// <summary> 1-无 2-默认 3-自定义 </summary>
        public int mode = 2;
        public string file;
    }

    public interface IAudioSystem : ISystem
    {
        void PlayAudio(AudioType audioType);
    }
    internal class AudioSystem : AbstractSystem, IAudioSystem
    {
        protected override void OnInit()
        {
        }
        public void PlayAudio(AudioType audioType)
        {
            int mode = AudioPlayModel(audioType);
            switch (mode)
            {
                case 2:
                    PlayDefaultAudio(audioType);
                    break;
                case 3:
                    PlayFileAudio(audioType);
                    break;
                case 1:
                default:
                    break;
            }
        }
        private int AudioPlayModel(AudioType audioType)
        {
            var appConfig = this.GetModel<AppConfig>();
            switch (audioType)
            {
                case AudioType.Click: return appConfig.audioClick.Value.mode;
                case AudioType.Group: return appConfig.audioGroup.Value.mode;
                case AudioType.Drop: return appConfig.audioDrop.Value.mode;
                case AudioType.Button: return appConfig.audioButton.Value.mode;
                case AudioType.None:
                default: return 0;
            }
        }
        private void PlayDefaultAudio(AudioType audioType)
        {
            UnmanagedMemoryStream file = null;
            switch (audioType)
            {
                case AudioType.Click: file = Resources.WAVE_CLICK; break;
                case AudioType.Group: file = Resources.WAVE_GROUP; break;
                case AudioType.Drop: file = Resources.WAVE_DROP; break;
                case AudioType.Button: file = Resources.WAVE_BUTTON; break;
                case AudioType.None:
                default: break;
            }
            if (file == null)
                return;
            SoundPlayer soundPlayer = new SoundPlayer(file);
            soundPlayer.Play();
        }
        private void PlayFileAudio(AudioType audioType)
        {
            var appConfig = this.GetModel<AppConfig>();
            string file = string.Empty;
            switch (audioType)
            {
                case AudioType.Click: file = appConfig.audioClick.Value.file; break;
                case AudioType.Group: file = appConfig.audioGroup.Value.file; break;
                case AudioType.Drop: file = appConfig.audioDrop.Value.file; break;
                case AudioType.Button: file = appConfig.audioButton.Value.file; break;
                case AudioType.None:
                default: break;
            }
            if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
                return;
            SoundPlayer soundPlayer = new SoundPlayer(file);
            soundPlayer.Play();
        }
    }
}
