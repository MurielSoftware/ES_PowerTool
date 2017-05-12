using Desktop.Ui.Core.Handlers;
using Desktop.Ui.Core.Windows;
using ES_PowerTool.ModelViews;
using ES_PowerTool.Shared;
using ES_PowerTool.Shared.CSV;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;

namespace ES_PowerTool.Handlers
{
    public class NewProjectHandler : NewEntityHandler<ProjectDto>
    {
        //protected override void DoExecute(ExecutionEvent executionEvent)
        //{
        //    NewProjectModelView newProjectModelView = new NewProjectModelView();
        //    WindowsManager.GetInstance().ShowWizard<Wizard>(newProjectModelView);
        //    ServiceActivator serviceActivator = new ServiceActivator();

        //    ICompositeTypeCRUDService compositeTypeCRUDService = serviceActivator.Get<ICompositeTypeCRUDService>();
        //    compositeTypeCRUDService.Read(Guid.NewGuid());
        //}

        //protected override void OnFailure(ExecutionEvent executionEvent)
        //{
        //}

        //protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        //{
        //}

        //protected override void BeforePersist(ExecutionEvent executionEvent, ProjectDto projectDto)
        //{
        //    if (projectDto.PathFolder == null || projectDto.PathType == null || projectDto.PathTypeElement == null)
        //    {
        //        return;
        //    }
        //    projectDto.CsvFolders = CSVFile.Load(projectDto.PathFolder.Path);
        //    projectDto.CsvTypes = CSVFile.Load(projectDto.PathType.Path);
        //    projectDto.CsvTypeElements = CSVFile.Load(projectDto.PathTypeElement.Path);
        //}
    }
}
