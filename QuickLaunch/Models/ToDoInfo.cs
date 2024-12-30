using QuickLinker.QuickLaunch.Constant;
using System;

namespace QuickLinker.QuickLaunch.Models
{
    [Serializable]
    public class ToDoInfo
    {
        //private string id;   //任务唯一id
        private string title; //待办事项
        private string msg;  //事项详情
        private string exeTime;  //待办时间
        private string doneTime; //完成时间
        private TodoTaskExecType execType = TodoTaskExecType.SET_TIME;
        private string cron;  //cron表达式
        //private int status;  //状态 0 未处理  1 已处理
    }
}
