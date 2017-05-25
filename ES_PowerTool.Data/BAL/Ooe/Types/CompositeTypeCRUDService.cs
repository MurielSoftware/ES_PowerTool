using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using ES_PowerTool.Shared.Services.OOE.Types;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using Desktop.Shared.Core;
using ES_PowerTool.Data.BAL.OOE.Types;

namespace ES_PowerTool.Data.BAL.OOE.Types
{
    public class CompositeTypeCRUDService : GenericCRUDService<CompositeTypeDto, CompositeType>, ICompositeTypeCRUDService
    {
        private CompositeTypeValidationService _compositeTypeValidationService;

        public CompositeTypeCRUDService(Connection connection) 
            : base(connection)
        {
            _compositeTypeValidationService = new CompositeTypeValidationService(connection);
        }

        protected override void ValidationBeforeDelete(CompositeType compositeType)
        {
            ValidationResult validationResult = _compositeTypeValidationService.CollectValidationResultBeforeDelete(compositeType);
            if (!validationResult.IsEmpty())
            {
                throw new ValidationException(validationResult);
            }
        }

        protected override CompositeType DoPersist(CompositeType compositeType)
        {
            bool compositeTypeExisted = EntityExists(compositeType);
            compositeType = base.DoPersist(compositeType);
            if(!compositeTypeExisted)
            {
                Preset preset = new Preset();
                preset.Name = "System";
                preset.TypeId = compositeType.Id;
                preset.ProjectId = compositeType.ProjectId;
                preset.State = State.NEW;
                preset = _genericRepository.Persist<Preset>(preset);

                compositeType.DefaultPresetId = preset.Id;
                _genericRepository.Persist<CompositeType>(compositeType);
            }
            return compositeType;
        }

        protected override void DoDelete(CompositeType compositeType)
        {
            _genericRepository.DeleteRange<CompositeTypeElement>(x => x.OwningTypeId == compositeType.Id);
            _genericRepository.DeleteRange<Preset>(x => x.TypeId == compositeType.Id);
            base.DoDelete(compositeType);
        }
    }
}
