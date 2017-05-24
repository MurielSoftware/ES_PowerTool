using Desktop.App.Core.Commands;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Desktop.App.Core.ModelViews
{
    public class BaseReferenceWindowModelView : BaseModelView
    {
        public TreeNavigationItem SelectedObject { get; set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }
        public ICommand FilterChangedCommand { get; private set; }
        public ICollectionView Proposals { get; private set; }

        public BaseReferenceWindowModelView()
            : base("BaseReferenceWindowModelView")
        {
            FilterChangedCommand = new RelayCommand(OnFilterChangedCommand);
            FinishCommand = new RelayCommand(OnFinishCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
        }

        public void LoadProposals(List<TreeNavigationItem> proposals)
        {
            Proposals = CollectionViewSource.GetDefaultView(proposals);
            Proposals.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            OnPropertyChanged(() => Proposals);
        }

        protected virtual void OnCancelCommand(object obj)
        {
            ReferenceWindow referenceWindow = (ReferenceWindow)obj;
            referenceWindow.DialogResult = false;
            referenceWindow.Close();
        }

        protected virtual void OnFinishCommand(object obj)
        {
            ReferenceWindow referenceWindow = (ReferenceWindow)obj;
            referenceWindow.DialogResult = true;
            referenceWindow.Close();
        }

        protected virtual void OnFilterChangedCommand(object obj)
        {
            if(string.IsNullOrEmpty(obj.ToString()))
            {
                Proposals.Filter = null;
            }

            Proposals.Filter = x =>
            {
                TreeNavigationItem treeNavigationItem = (TreeNavigationItem)x;
                if (treeNavigationItem == null)
                {
                    return false;
                }
                return treeNavigationItem.Name.ToLower().Contains(obj.ToString().ToLower());
            };
        }
    }
}
