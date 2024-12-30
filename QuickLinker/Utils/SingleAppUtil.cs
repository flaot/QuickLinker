using QFramework;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace QuickLinker.Utils
{
    public class SingleAppUtil : IUtility
    {
        //确保程序只运行一个实例
        public Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processesClient = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表
            foreach (Process process in processesClient)
            {
                //如果实例已经存在,则忽略当前进程
                if (process.Id != current.Id)
                {
                    if (process.MainModule.FileName == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null; //找不到其他进程实例，返回nulL。               
        }
        public void HandleRunningInstance(Process instance)
        {
            ShowWindow(instance.MainWindowHandle, SW_SHOWNOMAL);//调用API函数,正常显示窗口
            SetForegroundWindow(instance.MainWindowHandle);//将窗口放置在最前端  
        }
        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态  
        /// </summary>  
        /// <param name="hWnd">窗口句柄</param>  
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表</param>  
        /// <returns>如果窗口原来可见，返回值为非零；如果窗口原来被隐藏，返回值为零</returns>                      
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        private const int SW_SHOWNOMAL = 1;

        /// <summary>  
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。   
        /// </summary>  
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>  
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>  
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
