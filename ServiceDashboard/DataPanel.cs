﻿using System;
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

namespace DomainViewLite
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
    ///     <MyNamespace:DataPanel/>
    ///
    /// </summary>
    public class DataPanel : ContentControl
    {

        private Button addButton;
        private Button removeButton;

        public static readonly RoutedEvent OnAddClickedEvent =
            EventManager.RegisterRoutedEvent("OnAddClicked", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DataPanel));

        public static readonly RoutedEvent OnRemoveClickedEvent =
            EventManager.RegisterRoutedEvent("OnRemoveClicked", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(DataPanel));

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string),
            typeof(DataPanel), new FrameworkPropertyMetadata("DataPanel",
        FrameworkPropertyMetadataOptions.AffectsRender |
        FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        static DataPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataPanel), new FrameworkPropertyMetadata(typeof(DataPanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            addButton = GetTemplateChild("PART_AddItem") as Button;
            removeButton = GetTemplateChild("PART_RemoveItem") as Button;

            if (addButton != null) addButton.Click += new RoutedEventHandler(addButton_Click);
            if (removeButton != null) removeButton.Click += new RoutedEventHandler(removeButton_Click);
        }

        void removeButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnRemoveClickedEvent));
        }

        void addButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnAddClickedEvent));
        }

        public event RoutedEventHandler OnRemoveClicked
        {
            add { AddHandler(OnRemoveClickedEvent, value); }
            remove { RemoveHandler(OnRemoveClickedEvent, value); }
        }

        public event RoutedEventHandler OnAddClicked
        {
            add { AddHandler(OnAddClickedEvent, value); }
            remove { RemoveHandler(OnAddClickedEvent, value); }
        }

    }
}
