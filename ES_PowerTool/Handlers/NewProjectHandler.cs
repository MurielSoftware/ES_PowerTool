using Desktop.App.Core.Handlers;
using ES_PowerTool.Shared.Dtos;
using System;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Persister;
using Desktop.App.Core.Events.Publishing;
using Desktop.Shared.Core.Context;
using ES_PowerTool.ModelViews;

namespace ES_PowerTool.Handlers
{
    public class NewProjectHandler : NewEntityHandler<ProjectDto>
    {
        protected override WizardModelView<ProjectDto> CreateWizardModelView(ExecutionEvent executionEvent)
        {
            return new NewProjectModelView(CreateNewDto(executionEvent));
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            ModelViewsUtil.ProjectIsActive = true;
            Publisher.GetInstance().ServerChanged(Connection.GetInstance());
        }
    }
}
