﻿using Desktop.Shared.Core.Dtos;
using Desktop.App.Core.Ui.Builders;
using Desktop.App.Core.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.App.Core.Ui.Windows
{
    /// <summary>
    /// Interaction logic for Wizard.xaml
    /// </summary>
    public partial class Wizard : Window
    {
        private UiCreatorFactory _uiCreatorFactory = new UiCreatorFactory();

        public Wizard()
        {
            InitializeComponent();
        }

        internal void CreateUi()
        {
            _uiCreatorFactory.Generate(MainGrid, ((IWizardModelView)DataContext).GetDto());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = FindVisualChild<TextBox>(this);
            if(textBox == null)
            {
                return;
            }
            textBox.Focus();
        }

        private static T FindVisualChild<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                    {
                        return childItem;
                    }
                }
            }
            return null;
        }
    }
}
