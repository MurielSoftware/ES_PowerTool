using Desktop.App.Core.Commands;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.App.Core.ModelViews
{
    public class BaseReferenceWindowModelView : BaseModelView
    {
        public List<TreeNavigationItem> Proposals { get; private set; }
        public TreeNavigationItem SelectedObject { get; set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }

        public BaseReferenceWindowModelView()
            : base("BaseReferenceWindowModelView")
        {
            FinishCommand = new RelayCommand(OnFinishCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
        }

        public void LoadProposals(List<TreeNavigationItem> proposals)
        {
            Proposals = proposals;
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
    }
}
