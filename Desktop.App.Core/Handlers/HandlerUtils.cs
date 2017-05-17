using Desktop.Shared.Core.Navigations;
using ES_PowerTool.Shared.Dtos;
using ES_PowerTool.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Register(NavigationType.TYPE, typeof(CompositeTypeDto), typeof(ICompositeTypeCRUDService));
            Register(NavigationType.TYPE_ELEMENT, typeof(CompositeTypeElementDto), typeof(ICompositeTypeElementCRUDService));
        }

        private static void Register(NavigationType type, Type dto, Type service)
        {
            DTO_TO_SERVICE.Add(dto, service);
            TYPE_TO_DTO.Add(type, dto);
        }
    }
}
