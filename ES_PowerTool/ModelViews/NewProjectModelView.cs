using Desktop.Shared;
using Desktop.Ui.Core.ModelViews;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;

namespace ES_PowerTool.ModelViews
{
    public class NewProjectModelView : WizardModelView<ProjectDto>
    {
        protected override void OnFinishCommand(object obj)
        {
            base.OnFinishCommand(obj);

            ServiceActivator.Get<ICompositeTypeCRUDService>().Persist(new CompositeTypeDto() { Description = "Ahoj" });
        }
    }
}
