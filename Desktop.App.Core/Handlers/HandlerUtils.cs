using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Dtos.OOE;
using ES_PowerTool.Shared.Dtos.OOE.Elements;
using ES_PowerTool.Shared.Dtos.OOE.Presets;
using ES_PowerTool.Shared.Dtos.OOE.Types;
using ES_PowerTool.Shared.Services;
using ES_PowerTool.Shared.Services.OOE;
using ES_PowerTool.Shared.Services.OOE.Elements;
using ES_PowerTool.Shared.Services.OOE.Presets;
using ES_PowerTool.Shared.Services.OOE.Types;
using System;
using System.Collections.Generic;

namespace Desktop.App.Core.Handlers
{
    public static class HandlerUtils
    {
        public static Dictionary<Type, Type> DTO_TO_SERVICE = new Dictionary<Type, Type>();
        public static Dictionary<NavigationType, Type> TYPE_TO_DTO = new Dictionary<NavigationType, Type>();

        static HandlerUtils()
        {
            RegisterAll();
        }

        public static void RegisterAll()
        {
            Register(NavigationType.PROJECT, typeof(ProjectDto), typeof(IProjectCRUDService));
            Register(NavigationType.FOLDER, typeof(FolderDto), typeof(IFolderCRUDService));
            Register(NavigationType.COMPOSITE_TYPE, typeof(CompositeTypeDto), typeof(ICompositeTypeCRUDService));
            Register(NavigationType.TYPE_ELEMENT, typeof(CompositeTypeElementDto), typeof(ICompositeTypeElementCRUDService));
            Register(NavigationType.PRESET, typeof(PresetDto), typeof(IPresetCRUDService));
        }

        private static void Register(NavigationType type, Type dto, Type service)
        {
            DTO_TO_SERVICE.Add(dto, service);
            TYPE_TO_DTO.Add(type, dto);
        }
    }
}
