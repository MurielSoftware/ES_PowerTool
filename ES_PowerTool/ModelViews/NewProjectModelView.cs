using Desktop.Shared;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Services;
using Desktop.App.Core.Events.Publishing;
using Desktop.App.Core.Handlers;
using Desktop.App.Core.Jobs;
using Desktop.App.Core.ModelViews;
using ES_PowerTool.Persister;
using ES_PowerTool.Shared.CSV;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;

namespace ES_PowerTool.ModelViews
{
    public class NewProjectModelView : WizardModelView<ProjectDto>
    {
        public NewProjectModelView(ProjectDto projectDto) 
            : base(projectDto)
        {
        }

        protected override void DoFinish(ProjectDto projectDto)
        {
            Connection.GetInstance().CreateConnection(projectDto.Name + ".sdf");
            _crudService = (IProjectCRUDService)ServiceActivator.Get(typeof(IProjectCRUDService));
            _persister = new ProjectPersister(_crudService, projectDto);
            base.DoFinish(projectDto);
        }
    }
}
