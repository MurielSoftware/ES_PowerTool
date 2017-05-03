using Desktop.Shared.Core.Dtos;
using Desktop.Ui.Core.Builders;
using Desktop.Ui.Core.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.Ui.Core.Windows
{
    /// <summary>
    /// Interaction logic for Wizard.xaml
    /// </summary>
    public partial class Wizard : Window
    {
        private UiCreatorFactory _uiCreatorFactory = new UiCreatorFactory();

        public Wizard()
        {
            InitializeComponent();
        }

        internal void CreateUi()
        {
            _uiCreatorFactory.Generate(MainGrid, ((IWizardModelView)DataContext).GetDto());
        }
    }
}
