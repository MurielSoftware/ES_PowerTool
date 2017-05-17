using Desktop.Shared.Core.Context;
using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ES_PowerTool.Handlers
{
    public class OpenProjectHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            string fileName = SystemDialogUtils.ShowOpenFileDialog("*SQL Database file|*.sdf");
            if(string.IsNullOrEmpty(fileName))
            {
                return;
            }

            fileName = fileName.Replace(AppDomain.CurrentDomain.BaseDirectory, "");
            Connection.GetInstance().CreateConnection(fileName);
            Publisher.GetInstance().ServerChanged(null);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            throw new NotImplementedException();
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            throw new NotImplementedException();
        }
    }
}
