using Desktop.Ui.Core.ModelViews;
using ES_PowerTool.Shared.Dtos;

namespace ES_PowerTool.ModelViews
{
    public class NewProjectModelView : WizardModelView<ProjectDto>
    {
        protected override void OnFinishCommand(object obj)
        {
            base.OnFinishCommand(obj);
        }
    }
}
