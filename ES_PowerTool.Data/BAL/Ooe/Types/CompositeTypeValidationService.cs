using Desktop.Shared.Core.Services;
using Desktop.Shared.Core.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Ui.I18n;
using Desktop.Data.Core.Model;
using ES_PowerTool.Data.DAL.OOE.Types;
using ES_PowerTool.Data.DAL.OOE.Elements;

namespace ES_PowerTool.Data.BAL.OOE.Types
{
    public class CompositeTypeValidationService : BaseService
    {
        private GenericRepository _genericRepository;
        private CompositeTypeRepository _compositeTypeRepository;
        private CompositeTypeElementRepository _compositeTypeElementRepository;

        public CompositeTypeValidationService(Connection connection) 
            : base(connection)
        {
            _genericRepository = new GenericRepository(connection);
            _compositeTypeRepository = new CompositeTypeRepository(connection);
            _compositeTypeElementRepository = new CompositeTypeElementRepository(connection);
        }

        public ValidationResult CollectValidationResultBeforeDelete(CompositeType compositeType)
        {
            ValidationResult validationResult = new ValidationResult();
            List<CompositeTypeElement> compositeTypeElementWhereTypeIsUsedAsElementType = _compositeTypeElementRepository.FindCompositeTypeElementsToElementTypeId(compositeType.Id);
            if (compositeTypeElementWhereTypeIsUsedAsElementType.Count != 0)
            {
                validationResult.AddRange(CreateValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_TYPE_ELEMENT, compositeTypeElementWhereTypeIsUsedAsElementType, compositeType));
            }
            List<CompositeType> compositeTypesWhereTypeIsUsedAsSuperType = _compositeTypeRepository.FindCompositeTypesWhereIsUsedTypeInSuperTypes(compositeType.Id);
            if(compositeTypesWhereTypeIsUsedAsSuperType.Count != 0)
            {
                validationResult.AddRange(CreateValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_SUPER_TYPE, compositeTypesWhereTypeIsUsedAsSuperType, compositeType));
            }
            return validationResult;
        }

        private List<ValidationMessage> CreateValidationMessage(ValidationType validationType, string resourceKey, List<CompositeTypeElement> compositeTypeElements, CompositeType compositeTypeToValidate)
        {
            List<ValidationMessage> validationMessages = new List<ValidationMessage>();
            foreach (CompositeTypeElement compositeTypeElement in compositeTypeElements)
            {
                validationMessages.Add(new ValidationMessage(validationType, resourceKey, compositeTypeToValidate.Description, compositeTypeElement.Description));
            }
            return validationMessages;
        }

        private List<ValidationMessage> CreateValidationMessage(ValidationType validationType, string resourceKey, List<CompositeType> compositeTypes, CompositeType compositeTypeToValidate)
        {
            List<ValidationMessage> validationMessages = new List<ValidationMessage>();
            foreach(CompositeType compositeType in compositeTypes)
            {
                validationMessages.Add(new ValidationMessage(validationType, resourceKey, compositeTypeToValidate.Description, compositeType.Description));
            }
            return validationMessages;
        }
    }
}
