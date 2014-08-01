using Dashboard.DataAccess;
using Dashboard.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dashboard.ViewModel
{
    class AllTasksViewModel : WorkspaceViewModel<TaskViewModel>
    {
        readonly RepositoryBase<ModelTask> taskRepository;

        private static AllTasksViewModel instance;

        public static AllTasksViewModel Instance
        {
            get 
            {
                if (instance == null)
                    instance = new AllTasksViewModel();
                return instance;
            }
        }

        public AllTasksViewModel()
        {
            DisplayName = "All Tasks";
            taskRepository = new RepositoryBase<ModelTask>(Path.Combine(DashboardFunctions.GetFolder(""), 
                "task_data.xml"));
            taskRepository.ItemAdded += new Action<ModelTask>(taskRepository_ItemAdded);
            taskRepository.Load();
            this.RequestDelete += new EventHandler(AllTasksViewModel_RequestDelete);
            this.RequestAdd += new EventHandler(AllTasksViewModel_RequestAdd);
            DispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            DispatcherTimer.Start();
        }

        void taskRepository_ItemAdded(ModelTask obj)
        {
            TaskViewModel viewModel = new TaskViewModel(obj, taskRepository);
            if (string.IsNullOrEmpty(viewModel.Description))
                viewModel.Description = viewModel.TaskName;
            this.AllItems.Add(viewModel);
            viewModel.DoWork(null);
        }

        void AllTasksViewModel_RequestAdd(object sender, EventArgs e)
        {
            SelectedItem = new TaskViewModel(ModelTask.Add(false), taskRepository);
            SelectedItem.IsAdding = true;
        }

        void AllTasksViewModel_RequestDelete(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                if (taskRepository.Remove(SelectedItem.Context, true, true))
                    this.AllItems.Remove(SelectedItem);
            }
        }

        protected override void OnTimerTick(object sender, EventArgs e)
        {
            foreach (TaskViewModel vm in AllItems)
            {
                vm.DoWork(null);
            }
        }

        protected override void OnDispose()
        {
            foreach (TaskViewModel tvm in this.AllItems)
                tvm.Dispose();
            taskRepository.ItemAdded -= this.taskRepository_ItemAdded;
        }

        public bool HasAlert
        {
            get
            {
                bool result = false;
                foreach (TaskViewModel vm in AllItems)
                {
                    if (vm.IsOnAlert)
                        result = true;
                }
                return result;
            }
        }
    }
}
