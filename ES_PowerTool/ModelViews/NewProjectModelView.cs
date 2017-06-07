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
using Desktop.Shared.Core.Jobs;
using ES_PowerTool.Settings;

namespace ES_PowerTool.ModelViews
{
    public class NewProjectModelView : WizardModelView<ProjectDto>
    {
        private bool _isDatabaseFileAlreadyExisting;

        public NewProjectModelView(ProjectDto projectDto) 
            : base(projectDto)
        {
        }

        protected override void DoPersist(ProgressCounter progressCounter, ProjectDto projectDto)
        {
            _isDatabaseFileAlreadyExisting = IsDatabaseFileExisting(projectDto.Name);
            Connection.GetInstance().CreateConnection(GetPathToDatabaseFile(projectDto.Name));
            _crudService = (IProjectCRUDService)ServiceActivator.Get(typeof(IProjectCRUDService));
            _persister = new ProjectPersister(_crudService, progressCounter, projectDto);
            _persister.Persist();
        }

        protected override void OnServerSideFailed(ProjectDto projectDto, ValidationResult validationResult)
        {
            base.OnServerSideFailed(projectDto, validationResult);
            if (!_isDatabaseFileAlreadyExisting)
            {
                File.Delete(GetPathToDatabaseFile(projectDto.Name));
            }
        }

        private static bool IsDatabaseFileExisting(string projectName)
        {
            return File.Exists(GetPathToDatabaseFile(projectName));
        }

        private static string GetPathToDatabaseFile(string projectName)
        {
            return ProjectProvider.WORKSPACE_DIRECTORY + CreateDatabaseFileName(projectName);
        }

        private static string CreateDatabaseFileName(string projectName)
        {
            return projectName + ".est";
        }
    }
}
