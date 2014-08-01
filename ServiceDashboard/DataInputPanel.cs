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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DomainViewLite"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:DomainViewLite;assembly=DomainViewLite"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:DataInputPanel/>
    ///
    /// </summary>
    public class DataInputPanel : ContentControl
    {
        private Button saveButton;
        private Button cancelButton;

        public static readonly RoutedEvent OnSaveClickedEvent =
            EventManager.RegisterRoutedEvent("OnSaveClicked", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DataInputPanel));

        public static readonly RoutedEvent OnCancelClickedEvent =
            EventManager.RegisterRoutedEvent("OnCancelClicked", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DataInputPanel));

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string),
            typeof(DataInputPanel), new FrameworkPropertyMetadata("DataInputPanel",
        FrameworkPropertyMetadataOptions.AffectsRender |
        FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }


        static DataInputPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataInputPanel), new FrameworkPropertyMetadata(typeof(DataInputPanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            saveButton = GetTemplateChild("PART_SaveItem") as Button;
            cancelButton = GetTemplateChild("PART_CancelItem") as Button;

            if (saveButton != null) saveButton.Click += new RoutedEventHandler(saveButton_Click);
            if (cancelButton != null) cancelButton.Click += new RoutedEventHandler(cancelButton_Click);
        }

        void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnCancelClickedEvent));
        }

        void saveButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnSaveClickedEvent));
        }

        public event RoutedEventHandler OnCancelClicked
        {
            add { AddHandler(OnCancelClickedEvent, value); }
            remove { RemoveHandler(OnCancelClickedEvent, value); }
        }

        public event RoutedEventHandler OnSaveClicked
        {
            add { AddHandler(OnSaveClickedEvent, value); }
            remove { RemoveHandler(OnSaveClickedEvent, value); }
        }
    }
}
