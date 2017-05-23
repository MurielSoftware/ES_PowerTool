using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using System;
using System.Collections.Generic;
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;

namespace ES_PowerTool.Data.BAL
{
    public class CompositeTypeCRUDService : GenericCRUDService<CompositeTypeDto, CompositeType>, ICompositeTypeCRUDService
    {
        public CompositeTypeCRUDService(Connection connection) 
            : base(connection)
        {
        }

        protected override void ValidationBeforeDelete(CompositeType compositeType)
        {
            ValidationResult validationResult = new ValidationResult();
            base.ValidationBeforeDelete(compositeType);
            if(_genericRepository.Exists<CompositeTypeElement>(x => x.ElementTypeId == compositeType.Id))
            {
                validationResult.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_TYPE_ELEMENT));
            }
            //if(_genericRepository.Exists<CompositeType>(x => x.SuperTypes.)
            //{
            //    validationResult.Add(new ValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_TYPE_IS_USED_AS_SUPER_TYPE));
            //}
            if (!validationResult.IsEmpty())
            {
                throw new ValidationException(validationResult);
            }
        }

        protected override void DoDelete(CompositeType compositeType)
        {
            _genericRepository.DeleteRange<CompositeTypeElement>(x => x.OwningTypeId == compositeType.Id);
            _genericRepository.DeleteRange<Preset>(x => x.TypeId == compositeType.Id);
            base.DoDelete(compositeType);
        }
    }
}
