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
using ES_PowerTool.Data.BAL.OOE.Presets;
using ES_PowerTool.Data.DAL.OOE.Presets;

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
            if (EntityExists(compositeTypeElement))
            {
                UpdateCompositePresetElementIfNecessarry(compositeTypeElement);
                compositeTypeElement = base.DoPersist(compositeTypeElement);
            }
            else
            {
                compositeTypeElement = base.DoPersist(compositeTypeElement);
                if (_compositeTypeRepository.IsTypeCompositeType(compositeTypeElement.ElementTypeId))
                {
                    AddAssociatedCompositePresetElement(compositeTypeElement);
                }
            }
            return compositeTypeElement;
        }

        private void UpdateCompositePresetElementIfNecessarry(CompositeTypeElement compositeTypeElement)
        {
            CompositeTypeElement oldCompositeTypeElement = _genericRepository.FindNoTracking<CompositeTypeElement>(compositeTypeElement.Id);
            if (compositeTypeElement.ElementTypeId.Equals(oldCompositeTypeElement.ElementTypeId))
            {
                return;
            }
            bool oldElementTypeIsComposite = _compositeTypeRepository.IsTypeCompositeType(oldCompositeTypeElement.ElementTypeId);
            bool newElementTypeIsComposite = _compositeTypeRepository.IsTypeCompositeType(compositeTypeElement.ElementTypeId);
            if(oldElementTypeIsComposite && !newElementTypeIsComposite)
            {
                RemoveAssociatedCompositePresetElement(compositeTypeElement);
            }
            else if(!oldElementTypeIsComposite && newElementTypeIsComposite)
            {
                AddAssociatedCompositePresetElement(compositeTypeElement);
            }
        }

        private void AddAssociatedCompositePresetElement(CompositeTypeElement compositeTypeElement)
        {
            CompositeType owningType = _genericRepository.Find<CompositeType>(compositeTypeElement.OwningTypeId);
            CompositeType elementType = _genericRepository.Find<CompositeType>(compositeTypeElement.ElementTypeId);
            foreach (Preset preset in owningType.Presets)
            {
                _compositePresetElementCRUDService.PersistFromCompositeTypeElement(compositeTypeElement, elementType, preset);
            }
        }

        private void RemoveAssociatedCompositePresetElement(CompositeTypeElement compositeTypeElement)
        {
            _genericRepository.DeleteRange<CompositePresetElement>(x => x.CompositeTypeElementId == compositeTypeElement.Id);
        }

        protected override void DoDelete(CompositeTypeElement compositeTypeElement)
        {
            RemoveAssociatedCompositePresetElement(compositeTypeElement);
            base.DoDelete(compositeTypeElement);
        }
    }
}
