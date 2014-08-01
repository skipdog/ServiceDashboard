using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.UserControls
{
    /// <summary>
    /// Interaction logic for DriveStatus.xaml
    /// </summary>
    public partial class DriveStatus : UserControl
    {

        public static readonly DependencyProperty colorIndexProperty = DependencyProperty.Register(
            "ColorIndex", typeof(int), typeof(DriveStatus), new PropertyMetadata(0, null));

        public static readonly DependencyProperty valueProperty = DependencyProperty.Register(
            "Value", typeof(string), typeof(DriveStatus), new PropertyMetadata("0",
                new PropertyChangedCallback(OnValuePropertyChanged)));

        public decimal Value
        {
            get { return (decimal)GetValue(valueProperty); }
            set { SetValue(valueProperty, value); }
        }

        public int ColorIndex
        {
            get { return (int)GetValue(colorIndexProperty); }
            set { SetValue(colorIndexProperty, value); }
        }

        public DriveStatus()
        {
            InitializeComponent();           
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {           
            DriveStatus current = (DriveStatus)d;
            Double barWidth = 0;
            try
            {
                double value = (e.NewValue == null) ? 0 : Convert.ToDouble(e.NewValue.ToString());
                if (value >=70)
                    current.ColorIndex = 1;
                if (value >= 90)
                    current.ColorIndex = 2;
                barWidth = (value / (double)current.Width) * 100;
            }
            catch { barWidth = 0; }
            current.ProgressBarForeground.Width = barWidth;
            current.ProgressText.Text = string.Format("{0}%", barWidth);
        }

    }

}
