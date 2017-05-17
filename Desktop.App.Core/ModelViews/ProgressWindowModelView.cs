using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Desktop.App.Core.ModelViews
{
    public class ProgressWindowModelView : BaseModelView
    {
        public string Title { get; set; }
        public int CurrentProgress { get; set; }
        public string CurrentStep { get; set; }

        public ImageSource InfoIcon
        {
            get { return Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Information.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()); }
        }

        public ProgressWindowModelView()
            : base("ProgressWindowModelView")
        {
        }

        public void UpdateProgress(string currentStep, int currentProgress)
        {
            CurrentProgress = currentProgress;
            CurrentStep = currentStep;
            OnPropertyChanged(() => CurrentProgress);
            OnPropertyChanged(() => CurrentStep);
        }
    }
}
