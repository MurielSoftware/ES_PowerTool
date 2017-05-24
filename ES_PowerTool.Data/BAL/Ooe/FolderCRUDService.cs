using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Services.OOE;
using ES_PowerTool.Shared.Dtos.OOE;
using Desktop.Shared.Core.Validations;
using ES_PowerTool.Data.BAL.Ooe;

namespace ES_PowerTool.Data.BAL.OOE
{
    public class FolderCRUDService : GenericCRUDService<FolderDto, Folder>, IFolderCRUDService
    {
        private FolderValidationService _folderValidationService;

        public FolderCRUDService(Connection connection) 
            : base(connection)
        {
            _folderValidationService = new FolderValidationService(connection);
        }

        protected override void ValidationBeforeDelete(Folder folder)
        {
            ValidationResult validationResult = _folderValidationService.CollectValidationResultBeforeDelete(folder);
            if (!validationResult.IsEmpty())
            {
                throw new ValidationException(validationResult);
            }
        }

        protected override void DoDelete(Folder entity)
        {
            //base.DoDelete(entity);
        }
    }
}
