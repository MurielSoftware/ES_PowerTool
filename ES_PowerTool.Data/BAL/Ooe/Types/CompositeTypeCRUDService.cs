using Desktop.Shared.Core.Context;
using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using ES_PowerTool.Shared.Services.OOE.Types;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using Desktop.Shared.Core;
using ES_PowerTool.Data.BAL.OOE.Types;
using System;
using ES_PowerTool.Data.DAL.OOE.Presets;
using System.Collections.Generic;

namespace ES_PowerTool.Data.BAL.OOE.Types
{
    public class CompositeTypeCRUDService : GenericCRUDService<CompositeTypeDto, CompositeType>, ICompositeTypeCRUDService
    {
        private CompositeTypeValidationService _compositeTypeValidationService;
        private PresetRepository _presetRepository;

        public CompositeTypeCRUDService(Connection connection) 
            : base(connection)
        {
            _compositeTypeValidationService = new CompositeTypeValidationService(connection);
            _presetRepository = new PresetRepository(connection);
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
            List<Guid> presetIds = _presetRepository.FindPresetIdsToCompositeTypeIds(new List<Guid>() { compositeType.Id });            
            _genericRepository.DeleteRange<CompositePresetElement>(x => presetIds.Contains(x.OwningPresetId));
            _genericRepository.DeleteRange<CompositeTypeElement>(x => x.OwningTypeId == compositeType.Id);
            _genericRepository.DeleteRange<Preset>(x => x.TypeId == compositeType.Id);
            base.DoDelete(compositeType);
        }

        public void Move(Guid sourceId, Guid targetId)
        {
            CompositeType sourceCompositeType = _genericRepository.Find<CompositeType>(sourceId);
            sourceCompositeType.FolderId = targetId;
            _genericRepository.Persist<CompositeType>(sourceCompositeType);
        }
    }
}
