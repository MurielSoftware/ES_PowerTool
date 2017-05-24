using Desktop.Data.Core.BAL;
using Desktop.Data.Core.Model;
using System;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Shared.Services.OOE.Presets;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using ES_PowerTool.Data.DAL.OOE.Types;
using ES_PowerTool.Data.BAL.Ooe.Presets;
using Desktop.Shared.Core.Validations;

namespace ES_PowerTool.Data.BAL.OOE.Presets
{
    public class PresetCRUDService : GenericCRUDService<PresetDto, Preset>, IPresetCRUDService
    {
        private CompositeTypeRepository _compositeTypeRepository;
        private CompositePresetElementCRUDService _compositePresetElementCRUDService;
        private PresetValidationService _presetValidationService;

        public PresetCRUDService(Connection connection) 
            : base(connection)
        {
            _compositePresetElementCRUDService = new CompositePresetElementCRUDService(connection);
            _compositeTypeRepository = new CompositeTypeRepository(connection);
            _presetValidationService = new PresetValidationService(connection);
        }

        public void SetAsDefault(Guid presetId, Guid owningTypeId)
        {
            CompositeType owningType = _genericRepository.Find<CompositeType>(owningTypeId);
            if(owningType.DefaultPresetId.Equals(presetId))
            {
                return;
            }
            owningType.DefaultPresetId = presetId;
            _genericRepository.Persist<CompositeType>(owningType);
        }

        protected override Preset DoPersist(Preset preset)
        {
            bool presetExisted = EntityExists(preset);
            preset = base.DoPersist(preset);
            if (!presetExisted)
            {
                if (_compositeTypeRepository.IsTypeCompositeType(preset.TypeId))
                {
                    CompositeType compositeType = _genericRepository.Find<CompositeType>(preset.TypeId);
                    if (!compositeType.DefaultPresetId.HasValue)
                    {
                        compositeType.DefaultPresetId = preset.Id;
                        _genericRepository.Persist<CompositeType>(compositeType);
                    }
                    if (compositeType.Children != null)
                    {
                        foreach (CompositeTypeElement compositeTypeElement in compositeType.Children)
                        {
                            if (_compositeTypeRepository.IsTypeCompositeType(compositeTypeElement.ElementTypeId))
                            {
                                CompositeType elementType = (CompositeType)compositeTypeElement.ElementType;
                                _compositePresetElementCRUDService.PersistFromCompositeTypeElement(compositeTypeElement, elementType, preset);
                            }
                        }
                    }
                }
            }
            return preset;
        }

        protected override void ValidationBeforeDelete(Preset preset)
        {
            ValidationResult validationResult = _presetValidationService.CollectValidationResultBeforeDelete(preset);
            if (!validationResult.IsEmpty())
            {
                throw new ValidationException(validationResult);
            }
        }

        protected override void DoDelete(Preset preset)
        {
            _genericRepository.DeleteRange<CompositePresetElement>(x => x.OwningPresetId == preset.Id);
            base.DoDelete(preset);
        }
    }
}
