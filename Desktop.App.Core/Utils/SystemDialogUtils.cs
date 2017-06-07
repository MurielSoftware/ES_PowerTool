using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.App.Core.Utils
{
    public class SystemDialogUtils
    {
        public static string ShowOpenFileDialog(string filter, string initPath = "")
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if(!string.IsNullOrEmpty(initPath))
                {
                    openFileDialog.InitialDirectory = initPath;
                }
                openFileDialog.Filter = filter;
                if(DialogResult.OK.Equals(openFileDialog.ShowDialog()))
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }
    }
}
