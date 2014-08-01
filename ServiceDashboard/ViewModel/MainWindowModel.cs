using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashboard.View;
using System.Windows;
using System.Collections.ObjectModel;
using ServiceDashboard;

namespace Dashboard.ViewModel
{
    class MainWindowModel : WorkspaceViewModel<Object>
    {        
        ReadOnlyCollection<CommandViewModel> commandList;
        AllServicesViewModel asvm = AllServicesViewModel.Instance;
        AllTasksViewModel atvm = AllTasksViewModel.Instance;
        private MainWindow mainWindow { get; set; }

        private bool isPaused;
        private bool IsPaused 
        {
            get { return isPaused; }
            set 
            { 
                isPaused = value;
                UpdateOverlay();
            }
        }

        public MainWindowModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            IsPaused = false;
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            DispatcherTimer.Start();
        }

        protected override void OnTimerTick(object sender, EventArgs e)
        {
            UpdateOverlay();
        }

        void UpdateOverlay()
        {
            bool alertOn = false;
            try
            {
                if (!IsPaused)
                {
                    foreach (ServiceViewModel svm in asvm.AllItems)
                    {
                        if (svm.IsOnAlert)
                        {
                            alertOn = true;
                            break;
                        }
                    }
                    if (!alertOn)
                    {
                        foreach (TaskViewModel tvm in atvm.AllItems)
                        {
                            if (tvm.IsOnAlert)
                            {
                                alertOn = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                alertOn = false;
            }
            DashboardFunctions.OverlayAlert(mainWindow, alertOn, IsPaused);
        }

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (commandList == null)
                    commandList = new ReadOnlyCollection<CommandViewModel>(CreateCommands());
                return commandList;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel("Paused",  new RelayCommand(param => PauseUnPause()), false, true),
                new CommandViewModel("Settings",  new RelayCommand(param => OpenSettings()), false, false)
            };
        }

        void OpenSettings()
        {
        }

        void PauseUnPause()
        {
            IsPaused = !IsPaused;
        }

    }
}
