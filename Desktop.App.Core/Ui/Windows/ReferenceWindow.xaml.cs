using Desktop.App.Core.ModelViews;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ReferenceWindow.xaml
    /// </summary>
    public partial class ReferenceWindow : Window
    {
        private Func<Task<List<TreeNavigationItem>>> _actionToGetProposals;
        private BaseReferenceWindowModelView _referenceWindowModelView;
        private ICollectionView proposalCollectionView;

        public ReferenceWindow()
        {
            InitializeComponent();
        }

        public ReferenceWindow(BaseReferenceWindowModelView referenceWindowModelView, Func<Task<List<TreeNavigationItem>>> actionToGetProposals)
            :this()
        {
            DataContext = referenceWindowModelView;
            _referenceWindowModelView = referenceWindowModelView;
            _actionToGetProposals = actionToGetProposals;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<TreeNavigationItem> proposals = await _actionToGetProposals.Invoke();
            proposalCollectionView = CollectionViewSource.GetDefaultView(proposals);
            _referenceWindowModelView.LoadProposals(proposals);
            FilterTextBox.Focus();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
