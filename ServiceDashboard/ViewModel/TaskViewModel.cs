using Dashboard.DataAccess;
using Dashboard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Input;

namespace Dashboard.ViewModel
{
    class TaskViewModel : WorkspaceViewModel<object>, IDataErrorInfo
    {
        readonly ModelTask task;
        readonly RepositoryBase<ModelTask> taskRepository;

        public TaskViewModel(ModelTask task, RepositoryBase<ModelTask> taskRepository)
        {
            base.DisplayName = "Add Task";
            if (task == null)
                throw new ArgumentNullException("task");
            if (taskRepository == null)
                throw new ArgumentNullException("taskRepository");
            this.task = task;
            this.taskRepository = taskRepository;
            this.RequestSave += new EventHandler(TaskViewModel_RequestSave);
            this.RequestClose += new EventHandler(TaskViewModel_RequestClose);            
        }

        void TaskViewModel_RequestClose(object sender, EventArgs e)
        {
            IsAdding = false;
        }

        void TaskViewModel_RequestSave(object sender, EventArgs e)
        {
            if (CanSave) Save();
        }

        public ModelTask Context
        {
            get { return task; }
        }

        public string MachineName
        {
            get { return task.MachineName; }
            set
            {
                if (task.MachineName != value)
                {
                    task.MachineName = value;
                    base.OnPropertyChanged("MachineName");
                    base.OnPropertyChanged("TaskNameList");
                }
            }
        }

        public string TaskName
        {
            get { return task.TaskName; }
            set
            {
                if (task.TaskName != value)
                {
                    task.TaskName = value;
                    base.OnPropertyChanged("TaskName");
                }
            }
        }

        public string Description
        {
            get { return task.Description; }
            set
            {
                task.Description = value;
                base.OnPropertyChanged("Description");
            }
        }

        public string LastRan
        {
            get { return task.LastRan; }
            set
            {
                task.LastRan = value;
                base.OnPropertyChanged("LastRan");
            }
        }

        public string NextRun
        {
            get { return task.NextRun; }
            set
            {
                task.NextRun = value;
                base.OnPropertyChanged("NextRun");
            }
        }

        public string AlertStatus
        {
            get { return task.AlertStatus; }
            set
            {
                task.AlertStatus = value;
                base.OnPropertyChanged("AlertStatus");
            }
        }

        public string TaskStatus
        {
            get { return task.TaskStatus; }
            set
            {
                task.TaskStatus = value;
                base.OnPropertyChanged("TaskStatus");
                base.OnPropertyChanged("IsOnAlert");
            }
        }

        public string UserName
        {
            get { return task.UserName; }
            set
            {
                task.UserName = value;
                base.OnPropertyChanged("UserName");
                base.OnPropertyChanged("Password");
            }
        }

        public string Password
        {
            get { return task.Password; }
            set
            {
                task.Password = value;
                base.OnPropertyChanged("Password");
            }
        }

        public bool IsOnAlert
        {
            get { return task.IsOnAlert; }
        }

        public ObservableCollection<string> TaskNameList
        {
            get { return new ObservableCollection<string>(GetTaskNameList()); }
        }

        public List<string> GetTaskNameList()
        {
            List<string> result = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(MachineName))
                {
                    TaskScheduler.TaskScheduler ts = new TaskScheduler.TaskScheduler();
                    if (!string.IsNullOrEmpty(UserName))
                        ts.Connect(MachineName, UserName, "", Password);
                    else
                        ts.Connect(MachineName);
                    TaskScheduler.ITaskFolder taskFolder = ts.GetFolder("");
                    TaskScheduler.IRegisteredTaskCollection collection = taskFolder.GetTasks(1);
                    foreach (TaskScheduler.IRegisteredTask rt in collection)
                    {
                        result.Add(rt.Name);
                    }
                }
            }
            catch
            {
                result.Clear();
            }
            return result;
        }

        public void Save()
        {
            if (!task.IsValid)
                throw new InvalidOperationException("There was a probem saving the task.");
            if (this.IsNewTask)
            {
                task.AutoUpdate = true;
                taskRepository.Add(task, true);
                CloseCommand.Execute(null);
            }
            else
            {
                taskRepository.Update(task, true);
                CloseCommand.Execute(null);
            }
            base.OnPropertyChanged("DisplayName");
        }

        bool IsNewTask
        {
            get { return !taskRepository.ContainsItem(task); }
        }

        bool CanSave
        {
            get { return task.IsValid; }
        }

        protected override void OnDoWork(object sender, DoWorkEventArgs e)
        {
            if (!task.AutoUpdate)
            {
                return;
            }
            try
            {
                TaskScheduler.TaskScheduler ts = new TaskScheduler.TaskScheduler();
                if (!string.IsNullOrEmpty(UserName))
                    ts.Connect(MachineName, UserName, "", Password);
                else
                    ts.Connect(MachineName);
                TaskScheduler.ITaskFolder taskFolder = ts.GetFolder("");
                TaskScheduler.IRegisteredTaskCollection collection = taskFolder.GetTasks(1);
                foreach (TaskScheduler.IRegisteredTask rt in collection)
                {
                    if (rt.Name == TaskName)
                    {
                        LastRan = rt.LastRunTime.ToString();
                        NextRun = rt.NextRunTime.ToString();
                        TaskStatus = (rt.State == TaskScheduler._TASK_STATE.TASK_STATE_DISABLED) ? "Disabled" : "Enabled";                        
                    }
                }
            }
            catch (Exception ex)
            {
                LastRan = ex.Message;
            }
        }

        string IDataErrorInfo.Error { get { return (task as IDataErrorInfo).Error; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;
                error = (task as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }

    }
}
