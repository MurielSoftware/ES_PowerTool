﻿using Desktop.Shared.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.ModelViews
{
    public interface IWizardModelView
    {
        BaseDto GetDto();
    }
}
