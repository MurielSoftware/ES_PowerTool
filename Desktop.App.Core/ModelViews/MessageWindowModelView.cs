using Desktop.App.Core.Commands;
using Desktop.App.Core.Ui.Windows;
using Desktop.Shared.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.App.Core.ModelViews
{
    public class MessageWindowModelView : BaseModelView
    {
        public ValidationResult ValidationResult { get; set; }
        public ICommand CloseCommand { get; private set; }

        public MessageWindowModelView(ValidationResult validationResult) 
            : base("MessageWindowModelView")
        {
            ValidationResult = validationResult;
            CloseCommand = new RelayCommand(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            ((MessageWindow)obj).Close();
        }
    }
}
