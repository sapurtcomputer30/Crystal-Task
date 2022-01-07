using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrystalSceduler
{

    public class CrystalTask
    {
        bool status = true;
        List<TaskFactory> tasks;
        #region Events
        public delegate void TaskExecutaion(CrystalSceduler.TaskFactory task);
        public event TaskExecutaion TaskExecuted;
        public delegate void TaskScedulation(TaskFactory task, TimeSpan ExecuteTime);
        public event TaskScedulation TaskScedulated;
        public delegate void TaskActions();
        public event TaskActions ScedulerStart;
        public event TaskActions ScedulerStop;
        #endregion

        public CrystalTask()
        {
            tasks = new List<TaskFactory>();
        }
        
        public async Task StartAsync()
        {
            if (ScedulerStart != null) { ScedulerStart(); }
            while (true)
            {
                if (status == false)
                {
                    break;
                }

                if (tasks != null || tasks.Count > 0)
                {
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        var currentTask = tasks[i];

                        if (DateTime.Now.Hour == tasks[i].ExcuteTime.Hours
                       && DateTime.Now.Minute == tasks[i].ExcuteTime.Minutes
                       && DateTime.Now.Second == tasks[i].ExcuteTime.Seconds)
                        {

                            if (TaskExecuted != null && currentTask.IsComplite == false)
                            {
                                TaskExecuted(currentTask);

                            }
                            currentTask.IsComplite = true;
                            tasks.Remove(currentTask);



                        }

                    }

                }

            }

        }

        public void Stop()
        {
            status = false;
            if (ScedulerStop != null)
            {
                ScedulerStop();
            }
        }
        public List<TaskFactory> GetTasks() => tasks;
        public void AddTask(TaskFactory task)
        {
            task.ExcuteTime = task.ExcuteTime.StripMilliseconds();
            tasks.Add(task);


            if (TaskScedulated != null)
                TaskScedulated(task, task.ExcuteTime);
        }
        public void RemoveTask(TaskFactory task)
        {
            if (tasks.Exists(f => f.TaskName == task.TaskName))
                tasks.Remove(task);
        }



    }

    public class TaskFactory
    {

        public ThreadStart Task { get; set; }

        public TimeSpan ExcuteTime { get; set; }

        public string TaskName { get; set; }

        public string TaskId { get; } = Guid.NewGuid().ToString("N");
        public bool IsComplite { get; set; }
    }
    public static class TimeExtensions
    {
        public static TimeSpan StripMilliseconds(this TimeSpan time)
        {
            return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
