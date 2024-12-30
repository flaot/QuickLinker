using System;
using System.IO;
using System.Windows.Forms;

namespace QuickLinker.Utils
{
    internal class FromUtil
    {
        public static void TextBoxFileFolder_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Array file = (Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储
            foreach (object I in file)
            {
                string str = I.ToString();
                FileInfo info = new FileInfo(str);
                if ((info.Attributes & FileAttributes.Directory) != 0)
                {
                    textBox.Text = str;
                    return;
                }
                if (File.Exists(str))
                {
                    textBox.Text = Path.GetDirectoryName(str);
                    return;
                }
            }
        }
        public static void TextBoxFileFolder_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件
            else
                e.Effect = DragDropEffects.None;
        }
        public static void TextBoxFilePath_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Array file = (Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储
            foreach (object I in file)
            {
                string str = I.ToString();
                FileInfo info = new FileInfo(str);
                if ((info.Attributes & FileAttributes.Directory) != 0)
                    continue;
                if (File.Exists(str))
                {
                    textBox.Text = str;
                    return;
                }
            }
        }
        public static void TextBoxFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件
            else
                e.Effect = DragDropEffects.None;
        }
        public static bool TextBoxFileFolderOrFile_DragDrop(object sender, DragEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Array file = (Array)e.Data.GetData(DataFormats.FileDrop);//将拖来的数据转化为数组存储
            foreach (object I in file)
            {
                string str = I.ToString();
                textBox.Text = str;
                return true;
            }
            return false;
        }
        public static void TextBoxFileFolderOrFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))    //判断拖来的是否是文件
                e.Effect = DragDropEffects.Link;                //是则将拖动源中的数据连接到控件
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
