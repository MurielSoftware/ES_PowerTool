using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using Desktop.Data.Core.BAL;
using ES_PowerTool.Shared.Services.OOE.Elements;
using ES_PowerTool.Shared.Dtos.OOE.Elements;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System;
using ES_PowerTool.Data.DAL.OOE.Types;
using Desktop.Shared.Core;
using ES_PowerTool.Data.BAL.Ooe.Presets;
using ES_PowerTool.Data.DAL.Ooe.Presets;

namespace ES_PowerTool.Data.BAL.OOE.Elements
{
    public class CompositeTypeElementCRUDService : GenericCRUDService<CompositeTypeElementDto, CompositeTypeElement>, ICompositeTypeElementCRUDService
    {
        private CompositeTypeRepository _compositeTypeRepository;
        private CompositePresetElementCRUDService _compositePresetElementCRUDService;

        public CompositeTypeElementCRUDService(Connection connection) 
            : base(connection)
        {
            _compositePresetElementCRUDService = new CompositePresetElementCRUDService(connection);
            _compositeTypeRepository = new CompositeTypeRepository(connection);
        }

        protected override CompositeTypeElement DoPersist(CompositeTypeElement compositeTypeElement)
        {
            compositeTypeElement = base.DoPersist(compositeTypeElement);
            if(_compositeTypeRepository.IsTypeCompositeType(compositeTypeElement.ElementTypeId))
            {
                CompositeType owningType = _genericRepository.Find<CompositeType>(compositeTypeElement.OwningTypeId);
                CompositeType elementType = _genericRepository.Find<CompositeType>(compositeTypeElement.ElementTypeId);
                foreach (Preset preset in owningType.Presets)
                {
                    _compositePresetElementCRUDService.PersistFromCompositeTypeElement(compositeTypeElement, elementType, preset);
                }
            }
            return compositeTypeElement;
        }

        protected override void DoDelete(CompositeTypeElement compositeTypeElement)
        {
            _genericRepository.DeleteRange<CompositePresetElement>(x => x.CompositeTypeElementId == compositeTypeElement.Id);
            base.DoDelete(compositeTypeElement);
        }
    }
}
