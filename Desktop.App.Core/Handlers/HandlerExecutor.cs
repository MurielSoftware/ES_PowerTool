using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Handlers
{
    public class HandlerExecutor
    {
        public static void Execute<T>(ExecutionEvent executionEvent) where T : BaseHandler
        {
            T handler = Activator.CreateInstance<T>();
            handler.Execute(executionEvent);
        }

        public static void Execute(Type handlerType, Type genericHandlerType, ExecutionEvent executionEvent)
        {
            Type handlerGenericType = handlerType.MakeGenericType(genericHandlerType);
            BaseHandler handler = (BaseHandler)Activator.CreateInstance(handlerGenericType);
            handler.Execute(executionEvent);
        }

        public static void ExecuteEx(Type handlerType, object obj)
        {
            List<TreeNavigationItem> selectedTreeNavigationItems = obj as List<TreeNavigationItem>;
            Type dtoType = HandlerUtils.TYPE_TO_DTO[selectedTreeNavigationItems.First().Type];
            Execute(handlerType, dtoType, ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        }

        //public static void DeleteExecute(object obj)
        //{
        //    //List<TreeNavigationItem> selectedTreeNavigationItems = obj as List<TreeNavigationItem>;
        //    //Type dtoType = ServiceMapper.GetDto(selectedTreeNavigationItems.First().NavigationItemType);
        //    //Execute(typeof(DeleteEntityHandler<>), dtoType, ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        //}

        //public static void PasteExecute(object obj)
        //{
        //    List<TreeNavigationItem> treeNavigationItemsToPaste = ClipboardUtils.GetTreeNavigationItemsFromClipboard();
        //    if (treeNavigationItemsToPaste == null)
        //    {
        //        return;
        //    }
        //    Type dtoType = ServiceMapper.GetDto(treeNavigationItemsToPaste.First().NavigationItemType);
        //    Execute(typeof(PasteHandler<>), dtoType, ExecutionEvent.Create(obj as List<TreeNavigationItem>));
        //}
    }
}
