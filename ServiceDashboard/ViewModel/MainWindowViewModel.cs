using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using Dashboard.DataAccess;
using Dashboard.Model;
using System.Windows.Controls;
using Dashboard.View;
using System.Windows;
using System.IO;

namespace Dashboard.ViewModel
{
    /// <summary>
    /// The ViewModel for the application's main window.
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel<object>
    {
               
        ReadOnlyCollection<CommandViewModel> commandList;

        public MainWindowViewModel(DockPanel dockPanel)
        {
            base.DisplayName = "Dashboard";
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
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    commandList = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return commandList;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    "Settings",
                    new RelayCommand(param => this.OpenSettings()), false, false)
            };
        }        

        void OpenSettings()
        {
            //
        }

    }
}