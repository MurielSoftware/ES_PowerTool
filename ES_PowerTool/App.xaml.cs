using ES_PowerTool.Settings;
using Log4N.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ES_PowerTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (!Directory.Exists(ProjectProvider.WORKSPACE_DIRECTORY))
            {
                Directory.CreateDirectory(ProjectProvider.WORKSPACE_DIRECTORY);
            }
            Log.Info("StartLogging");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Log.Exit();
        }
    }
}
