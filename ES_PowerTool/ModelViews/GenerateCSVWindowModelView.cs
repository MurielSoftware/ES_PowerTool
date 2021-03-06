﻿using Desktop.App.Core.Commands;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Dtos.Generate;
using ES_PowerTool.Ui.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ES_PowerTool.ModelViews
{
    public class GenerateCSVWindowModelView : BaseModelView
    {
        public GenerateDto Dto { get; protected set; }

        public ICommand CloseCommand { get; private set; }

        public GenerateCSVWindowModelView(GenerateDto generateDto) 
            : base("GenerateWindowModelView")
        {
            Dto = generateDto;
            CloseCommand = new RelayCommand(OnCloseCommand);
        }

        private void OnCloseCommand(object obj)
        {
            ((GenerateCSVWindow)obj).Close();
        }
    }
}
