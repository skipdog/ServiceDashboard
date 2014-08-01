using Dashboard.DataAccess;
using Dashboard.Model;
using System;
using System.Windows.Threading;
using System.IO;
using System.Windows.Input;
using System.Windows;
using System.ServiceProcess;

namespace Dashboard.ViewModel
{
    public class AllServicesViewModel : WorkspaceViewModel<ServiceViewModel>
    {
        readonly RepositoryBase<ModelService> serviceRepository;

        RelayCommand serviceStopCommand;
        RelayCommand serviceStartCommand;
        RelayCommand serviceRestartCommand;

        private static AllServicesViewModel instance;

        public static AllServicesViewModel Instance
        {
            get 
            {
                if (instance == null)
                    instance = new AllServicesViewModel();
                return instance;
            }
        }

        public AllServicesViewModel()
        {
            DisplayName = "All Services";
            serviceRepository = new RepositoryBase<ModelService>(Path.Combine(DashboardFunctions.GetFolder(""), 
                "service_data.xml"));
            serviceRepository.ItemAdded += new Action<ModelService>(serviceRepository_ItemAdded);
            serviceRepository.Load();
            this.RequestDelete += new EventHandler(AllServicesViewModel_RequestDelete);
            this.RequestAdd += new EventHandler(AllServicesViewModel_RequestAdd);
            DispatcherTimer.Start();            
        }

        void serviceRepository_ItemAdded(ModelService obj)
        {
            ServiceViewModel viewModel = new ServiceViewModel(obj, serviceRepository);
            if (string.IsNullOrEmpty(viewModel.Description))
                viewModel.Description = viewModel.ServiceName;
            this.AllItems.Add(viewModel);
            viewModel.DoWork(null);
        }        

        public bool HasAlert
        {
            get
            {
                bool result = false;
                foreach (ServiceViewModel vm in AllItems)
                {
                    if (vm.IsOnAlert)
                        result = true;
                }
                return result;
            }
        }

        public ICommand ServiceStopCommand
        {
            get
            {
                if (serviceStopCommand == null)
                    serviceStopCommand = new RelayCommand(param => this.OnServiceStopCommand());
                return serviceStopCommand;
            }
        }

        public ICommand ServiceStartCommand
        {
            get
            {
                if (serviceStartCommand == null)
                    serviceStartCommand = new RelayCommand(param => this.OnServiceStartCommand());
                return serviceStartCommand;
            }
        }

        public ICommand ServiceRestartCommand
        {
            get
            {
                if (serviceRestartCommand == null)
                    serviceRestartCommand = new RelayCommand(param => this.OnServiceRestartCommand());
                return serviceRestartCommand;
            }
        }

        void OnServiceStopCommand()
        {
            if (SelectedItem != null)
            {
                ServiceController sc = new ServiceController(SelectedItem.ServiceName, SelectedItem.MachineName);
                try
                {
                    if (sc.CanStop)
                        sc.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void OnServiceStartCommand()
        {
            if (SelectedItem != null)
            {
                ServiceController sc = new ServiceController(SelectedItem.ServiceName, SelectedItem.MachineName);
                try
                {
                    sc.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void OnServiceRestartCommand()
        {
            if (SelectedItem != null)
            {
                ServiceController sc = new ServiceController(SelectedItem.ServiceName, SelectedItem.MachineName);
                if (sc.CanStop)
                {
                    try
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        sc.Start();
                    }
                    catch (Exception ex)
                    { 
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        void AllServicesViewModel_RequestAdd(object sender, EventArgs e)
        {
            SelectedItem = new ServiceViewModel(ModelService.Add(false), serviceRepository);
            SelectedItem.IsAdding = true;
        }

        void AllServicesViewModel_RequestDelete(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                if (serviceRepository.Remove(SelectedItem.Context, true, true))
                    this.AllItems.Remove(SelectedItem);
            }
        }

        protected override void OnTimerTick(object sender, EventArgs e)
        {
            foreach (ServiceViewModel vm in AllItems)
            {         
                vm.DoWork(null);
            }
        }

        void viewModel_RequestServiceStop(object sender, EventArgs e)
        {
            ServiceViewModel viewModel = sender as ServiceViewModel;
            if (viewModel != null)
            {
                string sn = viewModel.ServiceName;
                MessageBox.Show(sn);
            }
        }

        protected override void OnDispose()
        {
            foreach (ServiceViewModel svm in this.AllItems)
                svm.Dispose();
            serviceRepository.ItemAdded -= serviceRepository_ItemAdded;
        }

    }
    
}
