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
using Desktop.Shared.Core.Validations;
using System.IO;

namespace ES_PowerTool.ModelViews
{
    public class NewProjectModelView : WizardModelView<ProjectDto>
    {
        public NewProjectModelView(ProjectDto projectDto) 
            : base(projectDto)
        {
        }

        protected override void DoPersist(ProjectDto projectDto)
        {
            Connection.GetInstance().CreateConnection(CreateDatabaseFileName(projectDto.Name));
            _crudService = (IProjectCRUDService)ServiceActivator.Get(typeof(IProjectCRUDService));
            _persister = new ProjectPersister(_crudService, projectDto);
            _persister.Persist();
        }

        protected override void OnServerSideFailed(ProjectDto projectDto, ValidationResult validationResult)
        {
            base.OnServerSideFailed(projectDto, validationResult);
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + CreateDatabaseFileName(projectDto.Name));
        }

        private static string CreateDatabaseFileName(string projectName)
        {
            return projectName + ".sdf";
        }
    }
}
