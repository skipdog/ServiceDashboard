using Dashboard.DataAccess;
using Dashboard.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceProcess;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace Dashboard.ViewModel
{
    public class ServiceViewModel : WorkspaceViewModel<object>, IDataErrorInfo
    {        

        readonly ModelService service;
        readonly RepositoryBase<ModelService> serviceRepository;

        public ServiceViewModel(ModelService service, RepositoryBase<ModelService> serviceRepository)
        {
            base.DisplayName = "Add Service";
            if (service == null)
                throw new ArgumentNullException("service");
            if (serviceRepository == null)
                throw new ArgumentNullException("serviceRepository");
            this.service = service;
            this.serviceRepository = serviceRepository;
            this.RequestSave += new EventHandler(ServiceViewModel_RequestSave);
            this.RequestClose += new EventHandler(ServiceViewModel_RequestClose);
        }

        void ServiceViewModel_RequestClose(object sender, EventArgs e)
        {
            IsAdding = false;
        }

        void ServiceViewModel_RequestSave(object sender, EventArgs e)
        {
            if (CanSave) Save();
        }

        public ModelService Context
        {
            get { return service; }
        }
      
        public string MachineName 
        {
            get { return service.MachineName; }
            set
            {
                if (service.MachineName != value)
                {
                    service.MachineName = value;
                    base.OnPropertyChanged("MachineName");
                    base.OnPropertyChanged("ServiceNameList");
                }
            }
        }
        
        public string ServiceName 
        {
            get { return service.ServiceName; }
            set
            {
                if (service.ServiceName != value)
                {
                    service.ServiceName = value;
                    base.OnPropertyChanged("ServiceName");
                }
            }
        }

        public string Description
        {
            get { return service.Description; }
            set
            {
                service.Description = value;
                base.OnPropertyChanged("Description");
            }
        }

        public string ServiceStatus
        {
            get { return service.ServiceStatus; }
            set 
            { 
                service.ServiceStatus = value;
                base.OnPropertyChanged("ServiceStatus");
                base.OnPropertyChanged("IsOnAlert");
            }
        }

        public string AlertStatus
        {
            get { return service.AlertStatus; }
            set
            {
                service.AlertStatus = value;
                base.OnPropertyChanged("AlertStatus");
            }
        }

        public bool IsOnAlert
        {
            get { return service.IsOnAlert; }
        }

        public ObservableCollection<string> ServiceNameList
        {
            get { return new ObservableCollection<string>(GetServiceNameList()); }
        }

        public List<string> GetServiceNameList()
        {
            List<string> result = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(MachineName))
                {
                    ServiceController[] services = ServiceController.GetServices(MachineName);
                    foreach (ServiceController service in services)
                    {
                        result.Add(service.ServiceName);
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
            if (!service.IsValid)
                throw new InvalidOperationException("There was a probem saving the service.");
            if (this.IsNewService)
            {
                service.AutoUpdate = true;
                serviceRepository.Add(service, true);
                CloseCommand.Execute(null);
            }
            else
            {
                serviceRepository.Update(service, true);
                CloseCommand.Execute(null);
            }
            base.OnPropertyChanged("DisplayName");
        }

        bool IsNewService
        {
            get { return !serviceRepository.ContainsItem(service); }
        }        

        bool CanSave
        {
            get { return service.IsValid; }
        }

        protected override void OnDoWork(object sender, DoWorkEventArgs e)
        {
            if (!service.AutoUpdate)
            {
                return;
            }
            try
            {
                ServiceController sc = new ServiceController(ServiceName, MachineName);
                ServiceStatus = sc.Status.ToString();
            }
            catch (Exception ex)
            {
                ServiceStatus = ex.Message;
            }
        }

        string IDataErrorInfo.Error { get { return (service as IDataErrorInfo).Error; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get 
            {
                string error = null;
                error = (service as IDataErrorInfo)[propertyName];
                CommandManager.InvalidateRequerySuggested();
                return error; 
            }
        }

    }

}
