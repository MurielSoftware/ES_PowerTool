using Desktop.Shared;
using Desktop.Ui.Core.Jobs;
using Desktop.Ui.Core.ModelViews;
using ES_PowerTool.Shared.CSV;
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

            LongRunningJob<ProjectDto> projectCreationJob = new LongRunningJob<ProjectDto>(CreateProject, "title");
            projectCreationJob.Execute(Dto);

            //Dto.CsvFolders = CSVFile.Load(Dto.PathFolder.Path);
            //Dto.CsvTypes = CSVFile.Load(Dto.PathType.Path);
            //Dto.CsvTypeElements = CSVFile.Load(Dto.PathTypeElement.Path);

            //IProjectCRUDService projectCRUDService = ServiceActivator.Get<IProjectCRUDService>();
            //projectCRUDService.Persist(Dto);
        }

        private void CreateProject(ProjectDto projectDto)
        {
            if(projectDto.PathFolder == null || projectDto.PathType == null || projectDto.PathTypeElement == null)
            {
                return;
            }
            projectDto.CsvFolders = CSVFile.Load(projectDto.PathFolder.Path);
            projectDto.CsvTypes = CSVFile.Load(projectDto.PathType.Path);
            projectDto.CsvTypeElements = CSVFile.Load(projectDto.PathTypeElement.Path);

            IProjectCRUDService projectCRUDService = ServiceActivator.Get<IProjectCRUDService>();
            projectCRUDService.Persist(projectDto);
        }
    }
}
