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
using ES_PowerTool.ModelViews;
using Log4N.Logger;
using ES_PowerTool.Settings;

namespace ES_PowerTool.Handlers
{
    public class OpenProjectHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            string fileName = SystemDialogUtils.ShowOpenFileDialog("*ES Power Tool Data File|*.est", ProjectProvider.WORKSPACE_DIRECTORY);
            if(string.IsNullOrEmpty(fileName))
            {
                return;
            }

          //  fileName = fileName.Replace(AppDomain.CurrentDomain.BaseDirectory, fileName);
            Connection.GetInstance().CreateConnection(fileName);
            
            OnSuccessful(executionEvent, Guid.Empty);
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            Log.Error("Error during the opening the project");
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            Publisher.GetInstance().ServerChanged(Connection.GetInstance());
            ProjectProvider.GetInstance().SetProjectActive(true);
        }
    }
}
