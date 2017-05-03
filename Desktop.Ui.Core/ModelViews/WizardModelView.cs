using Desktop.Shared.Core.Dtos;
using Desktop.Ui.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.Ui.Core.ModelViews
{
    public class WizardModelView<T> : BaseModelView where T : BaseDto
    {
        public T Dto { get; protected set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand FinishCommand { get; private set; }

        public WizardModelView() 
            : base(typeof(WizardModelView<>).Name)
        {
            Dto = Activator.CreateInstance<T>();
            FinishCommand = new RelayCommand(OnFinishCommand);
            CancelCommand = new RelayCommand(OnCancelCommand);
        }

        protected virtual void OnCancelCommand(object obj)
        {
        }

        protected virtual void OnFinishCommand(object obj)
        {
        }
    }
}
