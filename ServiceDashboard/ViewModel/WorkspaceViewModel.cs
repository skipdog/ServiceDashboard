using System;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Dashboard.DataAccess;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Dashboard.ViewModel
{
    /// <summary>
    /// This ViewModelBase subclass requests to be removed 
    /// from the UI when its CloseCommand executes.
    /// This class is abstract.
    /// </summary>
    public abstract class WorkspaceViewModel<T> : ViewModelBase
    {

        public BackgroundWorker BackgroundWorker { get; set; }
        public DispatcherTimer DispatcherTimer { get; set; }

        private Dock dockPosition;
        private ObservableCollection<T> allItems;
        private T selectedItem;
        private bool isAdding = false;

        public Dock DockPosition
        {
            get { return dockPosition; }
            set { dockPosition = value; }
        }

        public ObservableCollection<T> AllItems 
        {
            get { return allItems; }
            set 
            {
                if (allItems != value)
                {
                    allItems = value;
                    OnPropertyChanged("AllItems");
                }
            }
        }

        public T SelectedItem
        {
            get 
            {                
                return selectedItem; 
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public bool IsAdding
        {
            get { return isAdding; }
            set
            {
                isAdding = value;
                OnPropertyChanged("IsAdding");
            }
        }        

        RelayCommand closeCommand;
        RelayCommand addCommand;
        RelayCommand saveCommand;
        RelayCommand deleteCommand;

        protected WorkspaceViewModel()
        {
            AllItems = new ObservableCollection<T>();
            DockPosition = Dock.Top;
            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 5);  
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnTimerTick(sender, e);
        }

        protected virtual void OnTimerTick(object sender, EventArgs e)
        {
        }

        public void DoWork(object argument)
        {
            if (!BackgroundWorker.IsBusy)
            {
                BackgroundWorker.RunWorkerAsync(argument);
            }
        }

        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OnDoWork(sender, e);
        }

        protected virtual void OnDoWork(object sender, DoWorkEventArgs e)
        {
        }

        /// <summary>
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                    closeCommand = new RelayCommand(param => this.OnRequestClose());

                return closeCommand;
            }
        }

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                    addCommand = new RelayCommand(param => this.OnRequestAdd());

                return addCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                    deleteCommand = new RelayCommand(param => this.OnRequestDelete());
                return deleteCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new RelayCommand(param => this.OnRequestSave());
                return saveCommand;
            }
        }
        
        public event EventHandler RequestClose;
        public event EventHandler RequestAdd;
        public event EventHandler RequestDelete;
        public event EventHandler RequestSave;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        void OnRequestAdd()
        {
            EventHandler handler = this.RequestAdd;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        void OnRequestDelete()
        {
            EventHandler handler = this.RequestDelete;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        void OnRequestSave()
        {
            EventHandler handler = this.RequestSave;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            this.AllItems.Clear();
            this.BackgroundWorker.Dispose();
        }

    }
}