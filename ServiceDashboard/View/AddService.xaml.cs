using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ServiceProcess;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {

        internal static readonly DependencyProperty FullServiceListProperty = DependencyProperty.Register(
                    "FullServiceList", typeof(ObservableCollection<string>), typeof(AddService),
                    new FrameworkPropertyMetadata(new ObservableCollection<string>(),
                    (FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure)));

        public ObservableCollection<string> FullServiceList
        {
            get
            {
                return (ObservableCollection<string>)GetValue(FullServiceListProperty);
            }
            set
            {
                SetValue(FullServiceListProperty, value);
            }
        }

        public AddService()
        {
            InitializeComponent();
            this.DataContext = this;
            FullServiceList = new ObservableCollection<string>();
            tbServerName.Text = "localhost".ToUpper();
            GenerateFullServiceList();
        }

        private void tbServerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                tbServerName.Text = tbServerName.Text.ToUpper();
                GenerateFullServiceList();
            }
        }

        private void GenerateFullServiceList()
        {
            string machineName = tbServerName.Text;
            try
            {
                FullServiceList.Clear();
                cmbServiceName.SelectedIndex = -1;
                if (string.IsNullOrEmpty(machineName)) machineName = "localhost".ToUpper();
                ServiceController[] controllerList = ServiceController.GetServices(tbServerName.Text);
                foreach (ServiceController controller in controllerList)
                {
                    FullServiceList.Add(controller.ServiceName);
                }
            }
            catch { }
        }

        private void DataInputPanel_OnSaveClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void DataInputPanel_OnCancelClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
