using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Services;
using Desktop.Shared.Core.Validations;
using Desktop.Ui.I18n;
using ES_PowerTool.Data.DAL.Ooe.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.Ooe.Presets
{
    public class PresetValidationService : BaseService
    {
        private GenericRepository _genericRepository;
        private CompositePresetElementRepository _compositePresetElementRepository;

        public PresetValidationService(Connection connection) 
            : base(connection)
        {
            _genericRepository = new GenericRepository(connection);
            _compositePresetElementRepository = new CompositePresetElementRepository(connection);
        }

        public ValidationResult CollectValidationResultBeforeDelete(Preset preset)
        {
            ValidationResult validationResult = new ValidationResult();
            List<CompositePresetElement> compositePresetElements = _compositePresetElementRepository.FindCompositePresetElementToAssociatedPreset(preset.Id);
            if (compositePresetElements.Count != 0)
            {
                validationResult.AddRange(CreateValidationMessage(ValidationType.ERROR, MessageKeyConstants.VALIDATION_MESSAGE_PRESET_IS_ASSOCIATED_TO_PRESET_ELEMENT, compositePresetElements, preset));
            }
            return validationResult;
        }

        private List<ValidationMessage> CreateValidationMessage(ValidationType validationType, string resourceKey, List<CompositePresetElement> compositePresetElements, Preset preset)
        {
            List<ValidationMessage> validationMessages = new List<ValidationMessage>();
            foreach (CompositePresetElement compositePresetElement in compositePresetElements)
            {
                string name = compositePresetElement.CompositeTypeElement.Description + " : " + compositePresetElement.CompositeTypeElement.ElementType.Description + " -> " + compositePresetElement.PresetForTypeElement.Name;
                validationMessages.Add(new ValidationMessage(validationType, resourceKey, preset.Name, name));
            }
            return validationMessages;
        }
    }
}
