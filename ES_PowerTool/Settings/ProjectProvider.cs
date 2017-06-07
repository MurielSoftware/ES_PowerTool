using Desktop.App.Core;
using Desktop.App.Core.Events;
using Desktop.Shared.Core;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Settings
{
    public class ProjectProvider : Singleton<ProjectProvider>
    {
        public static string WORKSPACE_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory + "workspace\\";

        private bool _projectIsActive { get; set; }
        
        public void SetProjectActive(bool projectIsActive)
        {
            _projectIsActive = projectIsActive;
        }

        public bool IsProjectActive()
        {
            return _projectIsActive;
        }
    }
}
