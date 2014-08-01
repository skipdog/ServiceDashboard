using System;
using System.Windows.Input;

namespace Dashboard.ViewModel
{
    /// <summary>
    /// Represents an actionable item displayed by a View.
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        public ICommand Command { get; private set; }
        public bool IsCheckable { get; private set; }
        public bool IsChecked { get; set; }

        public CommandViewModel(string displayName, ICommand command, bool isChecked, bool isCheckable)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            base.DisplayName = displayName;
            this.Command = command;
            this.IsCheckable = isCheckable;
            this.IsChecked = isChecked;
        }

    }
}