using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using ES_PowerTool.Shared.Services.OOE;
using ES_PowerTool.Shared.Dtos.OOE;
using Desktop.Shared.Core.Validations;
using ES_PowerTool.Data.BAL.OOE;
using System;
using ES_PowerTool.Data.DAL.OOE;
using System.Collections.Generic;
using ES_PowerTool.Data.DAL.OOE.Types;
using ES_PowerTool.Data.DAL.OOE.Presets;

namespace ES_PowerTool.Data.BAL.OOE
{
    public class FolderCRUDService : GenericCRUDService<FolderDto, Folder>, IFolderCRUDService
    {
        private FolderValidationService _folderValidationService;
        private FolderRepository _folderRepository;
        private CompositeTypeRepository _compositeTypeRepository;
        private PresetRepository _presetRepository;

        public FolderCRUDService(Connection connection) 
            : base(connection)
        {
            _folderValidationService = new FolderValidationService(connection);
            _folderRepository = new FolderRepository(connection);
            _compositeTypeRepository = new CompositeTypeRepository(connection);
            _presetRepository = new PresetRepository(connection);
        }

        public void Move(Guid sourceId, Guid targetId)
        {
            Folder sourceFolder = _genericRepository.Find<Folder>(sourceId);
            Folder targetFolder = _genericRepository.Find<Folder>(targetId);
            if (targetFolder != null)
            {
                sourceFolder.ParentId = targetFolder.Id;
            }
            else
            {
                sourceFolder.ParentId = null;
            }
            _genericRepository.Persist<Folder>(sourceFolder);
        }

        protected override void ValidationBeforeDelete(Folder folder)
        {
            ValidationResult validationResult = _folderValidationService.CollectValidationResultBeforeDelete(folder);
            if (!validationResult.IsEmpty())
            {
                throw new ValidationException(validationResult);
            }
        }

        protected override void DoDelete(Folder folder)
        {
            List<Guid> folderAndSubFolderIds = _folderRepository.FindAllSubFolderIds(folder.Id);
            List<Guid> compositeTypeIds = _compositeTypeRepository.FindCompositeTypeIdsToFolderIds(folderAndSubFolderIds);
            List<Guid> presetIds = _presetRepository.FindPresetIdsToCompositeTypeIds(compositeTypeIds);

            _genericRepository.DeleteRange<CompositePresetElement>(x => presetIds.Contains(x.OwningPresetId));
            _genericRepository.DeleteRange<CompositeTypeElement>(x => compositeTypeIds.Contains(x.OwningTypeId));
            _genericRepository.DeleteRange<Preset>(x => compositeTypeIds.Contains(x.TypeId));
            _genericRepository.DeleteRange<CompositeType>(x => compositeTypeIds.Contains(x.Id));
            DeleteAllSubFolders(folder.Id);
        }

        private void DeleteAllSubFolders(Guid parentFolderId)
        {
            List<Guid> directlySubFolderIds = _folderRepository.FindDirectlySubFolderIds(parentFolderId);
            foreach (Guid subFolderId in directlySubFolderIds)
            {
                DeleteAllSubFolders(subFolderId);
            }
            _genericRepository.DeleteRange<Folder>(x => x.Id == parentFolderId);
        }
    }
}
