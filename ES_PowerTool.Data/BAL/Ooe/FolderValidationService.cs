using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Services;
using Desktop.Shared.Core.Validations;
using ES_PowerTool.Data.BAL.Ooe.Types;
using ES_PowerTool.Data.DAL.OOE;
using ES_PowerTool.Data.DAL.OOE.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.Ooe
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
            List<CompositeType> invalidCompositeTypes = new List<CompositeType>();
            Dictionary<Guid, List<CompositeType>> invalidCompositeTypesToFolder = new Dictionary<Guid, List<CompositeType>>();

            foreach (CompositeType compositeType in compositeTypes)
            {
                invalidCompositeTypes.AddRange(_compositeTypeRepository.FindCompositeTypesWhereIsUsedTypeInSuperTypes(compositeType.Id));
                invalidCompositeTypes.AddRange(_compositeTypeRepository.FindCompositeTypeOwnerOfCompositeTypeElementsToElementTypeId(compositeType.Id));
                //List<CompositeTypeElement> compositeTypeElementWhereTypeIsUsedAsElementType = _compositeTypeElementRepository.FindCompositeTypeElementsToElementTypeId(compositeType.Id);
            }
            foreach(CompositeType invalidCompositeType in invalidCompositeTypes)
            {
                if(!invalidCompositeTypesToFolder.ContainsKey(invalidCompositeType.FolderId))
                {
                    invalidCompositeTypesToFolder.Add(invalidCompositeType.FolderId, new List<CompositeType>());
                }
                invalidCompositeTypesToFolder[invalidCompositeType.FolderId].Add(invalidCompositeType);
            }
            foreach(Guid folderId in folderIds)
            {
                invalidCompositeTypesToFolder.Remove(folderId);
            }

            if(invalidCompositeTypesToFolder.Count == 0)
            {
                return validationResult;
            }

            //List<CompositeTypeElement> compositeTypeElementWhereTypeIsUsedAsElementType = _compositeTypeElementRepository.FindCompositeTypeElementsToElementTypeId(compositeType.Id);
            //if (compositeTypeElementWhereTypeIsUsedAsElementType.Count != 0)
            //{
            //    validationResult.AddRange(CreateValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_TYPE_ELEMENT, compositeTypeElementWhereTypeIsUsedAsElementType, compositeType));
            //}
            //List<CompositeType> compositeTypesWhereTypeIsUsedAsSuperType = _compositeTypeRepository.FindCompositeTypesWhereIsUsedTypeInSuperTypes(compositeType.Id);
            //if (compositeTypesWhereTypeIsUsedAsSuperType.Count != 0)
            //{
            //    validationResult.AddRange(CreateValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_SUPER_TYPE, compositeTypesWhereTypeIsUsedAsSuperType, compositeType));
            //}
            return validationResult;
        }

        
    }
}
