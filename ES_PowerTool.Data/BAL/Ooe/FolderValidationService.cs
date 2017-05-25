using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Services;
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using ES_PowerTool.Data.BAL.OOE.Types;
using ES_PowerTool.Data.DAL.OOE;
using ES_PowerTool.Data.DAL.OOE.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.OOE
{
    public class FolderValidationService : BaseService
    {
        private GenericRepository _genericRepository;
        private FolderRepository _folderRepository;
        private CompositeTypeRepository _compositeTypeRepository;
        private CompositeTypeValidationService _compositeTypeValidationService;

        public FolderValidationService(Connection connection) 
            : base(connection)
        {
            _genericRepository = new GenericRepository(connection);
            _folderRepository = new FolderRepository(connection);
            _compositeTypeRepository = new CompositeTypeRepository(connection);
            _compositeTypeValidationService = new CompositeTypeValidationService(connection);
        }

        public ValidationResult CollectValidationResultBeforeDelete(Folder folder)
        {
            ValidationResult validationResult = new ValidationResult();
            List<Guid> folderIds = _folderRepository.FindAllSubFolderIds(folder.Id);
            List<CompositeType> compositeTypes = _compositeTypeRepository.FindCompositeTypesToFolderIds(folderIds);
            List<ValidationMessage> validationMessages = CollectInvalidCompositeTypesUsedInSuperTypesValidationMessages(folderIds, compositeTypes);
            validationMessages.AddRange(CollectInvalidCompositeTypesOwnerOfCompositeTypeElementsUsedAsElementType(folderIds, compositeTypes));
            validationResult.AddRange(validationMessages);
            return validationResult;
        }

        private List<ValidationMessage> CollectInvalidCompositeTypesUsedInSuperTypesValidationMessages(List<Guid> folderIds, List<CompositeType> compositeTypes)
        {
            List<ValidationMessage> validationMessages = new List<ValidationMessage>();
            List<CompositeType> invalidCompositeTypes = GetInvalidCompositeTypesUsedInSuperTypes(compositeTypes);
            Dictionary<Guid, List<CompositeType>> invalidCompositeTypesToFolder = CreateInvalidCompositeTypesToFolder(invalidCompositeTypes);
            invalidCompositeTypesToFolder = RemoveValid(invalidCompositeTypesToFolder, folderIds);
            if(invalidCompositeTypesToFolder.Count == 0)
            {
                return validationMessages;
            }
            foreach(KeyValuePair<Guid, List<CompositeType>> item in invalidCompositeTypesToFolder)
            {
                foreach(CompositeType compositeType in item.Value)
                {
                    validationMessages.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_SUPER_TYPE_IN_FOLDER, compositeType.Description, compositeType.Folder.Name));
                }
            }
            return validationMessages;
        }

        private List<ValidationMessage> CollectInvalidCompositeTypesOwnerOfCompositeTypeElementsUsedAsElementType(List<Guid> folderIds, List<CompositeType> compositeTypes)
        {
            List<ValidationMessage> validationMessages = new List<ValidationMessage>();
            List<CompositeType> invalidCompositeTypes = GetInvalidCompositeTypesOwnerOfCompositeTypeElementsUsedAsElementType(compositeTypes);
            Dictionary<Guid, List<CompositeType>> invalidCompositeTypesToFolder = CreateInvalidCompositeTypesToFolder(invalidCompositeTypes);
            invalidCompositeTypesToFolder = RemoveValid(invalidCompositeTypesToFolder, folderIds);
            if (invalidCompositeTypesToFolder.Count == 0)
            {
                return validationMessages;
            }
            foreach (KeyValuePair<Guid, List<CompositeType>> item in invalidCompositeTypesToFolder)
            {
                foreach (CompositeType compositeType in item.Value)
                {
                    validationMessages.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_ELEMENT_TYPE_IN_FOLDER, compositeType.Description, compositeType.Folder.Name));
                }
            }
            return validationMessages;
        }

        private List<CompositeType> GetInvalidCompositeTypesUsedInSuperTypes(List<CompositeType> compositeTypes)
        {
            List<CompositeType> invalidCompositeTypes = new List<CompositeType>();           
            foreach (CompositeType compositeType in compositeTypes)
            {
                invalidCompositeTypes.AddRange(_compositeTypeRepository.FindCompositeTypesWhereIsUsedTypeInSuperTypes(compositeType.Id));
            }
            return invalidCompositeTypes;
        }

        private List<CompositeType> GetInvalidCompositeTypesOwnerOfCompositeTypeElementsUsedAsElementType(List<CompositeType> compositeTypes)
        {
            List<CompositeType> invalidCompositeTypes = new List<CompositeType>();
            foreach (CompositeType compositeType in compositeTypes)
            {
                invalidCompositeTypes.AddRange(_compositeTypeRepository.FindCompositeTypeOwnerOfCompositeTypeElementsToElementTypeId(compositeType.Id));
            }
            return invalidCompositeTypes;
        }

        private Dictionary<Guid, List<CompositeType>> CreateInvalidCompositeTypesToFolder(List<CompositeType> invalidCompositeTypes)
        {
            Dictionary<Guid, List<CompositeType>> invalidCompositeTypesToFolder = new Dictionary<Guid, List<CompositeType>>();
            foreach (CompositeType invalidCompositeType in invalidCompositeTypes)
            {
                if (!invalidCompositeTypesToFolder.ContainsKey(invalidCompositeType.FolderId))
                {
                    invalidCompositeTypesToFolder.Add(invalidCompositeType.FolderId, new List<CompositeType>());
                }
                invalidCompositeTypesToFolder[invalidCompositeType.FolderId].Add(invalidCompositeType);
            }
            return invalidCompositeTypesToFolder;
        }

        private Dictionary<Guid, List<CompositeType>> RemoveValid(Dictionary<Guid, List<CompositeType>> invalidCompositeTypesToFolder, List<Guid> folderIds)
        {
            folderIds.ForEach(x => invalidCompositeTypesToFolder.Remove(x));
            return invalidCompositeTypesToFolder;
        }
    }
}
