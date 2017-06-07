using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Jobs;
using Desktop.Shared.Core.Services;
using ES_PowerTool.Data.BAL.Projects.Import;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services.Projects;
using System.Collections.Generic;

namespace ES_PowerTool.Data.BAL.Projects
{
    public class ProjectImportService : BaseService, IProjectImportService
    {
        private ProjectCRUDService _projectCRUDService;

        private static List<ITransferService<ProjectDto>> _transferServices = CreateTransferServices();

        public ProjectImportService(Connection connection) 
            : base(connection)
        {
            _projectCRUDService = new ProjectCRUDService(connection);
        }

        public void Import(ProgressCounter progressCounter, ProjectDto projectDto)
        {
            ProjectDto persistedProjectDto = _projectCRUDService.Persist(projectDto);
            projectDto.Id = persistedProjectDto.Id;

            foreach(ITransferService<ProjectDto> transferService in _transferServices)
            {
                progressCounter.Message = transferService.GetMessage();
                transferService.DoWork(_connection, projectDto);
            }
        }

        private static List<ITransferService<ProjectDto>> CreateTransferServices()
        {
            List<ITransferService<ProjectDto>> transferServices = new List<ITransferService<ProjectDto>>();
            transferServices.Add(new FolderTransferService());
            transferServices.Add(new TypesTransferService());
            transferServices.Add(new CompositeTypeElementsTransferService());
            transferServices.Add(new PresetTransferService());
            transferServices.Add(new PresetElementsTransferService());
            transferServices.Add(new DefaultPresetTransferService());
            transferServices.Add(new SuperTypeTransferService());
            transferServices.Add(new SettingsTransferService());
            return transferServices;
        }
    }
}
