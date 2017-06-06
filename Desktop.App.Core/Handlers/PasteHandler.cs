using Desktop.Shared.Core;
using Desktop.Shared.Core.Dtos;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop.App.Core.Handlers
{
    public class PasteHandler<T> : BaseHandler where T : BaseDto
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            ICRUDService<T> crudService = (ICRUDService<T>)ServiceActivator.Get(HandlerUtils.DTO_TO_SERVICE[typeof(T)]);
            List<T> dtos = new List<T>();
            foreach (TreeNavigationItem treeNavigationItem in GetTreeNavigationItemsFromClipboard("copy"))
            {
                dtos.Add(crudService.Read(treeNavigationItem.Id));
            }
            //Paste(dtos);
            //OnSuccessful(executionEvent, affectedObjectId);
        }

        //protected abstract void BeforePaste();
        //protected abstract void Paste(List<T> dtos);

        protected static List<TreeNavigationItem> GetTreeNavigationItemsFromClipboard(string format)
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject == null)
            {
                return null;
            }
            if (!dataObject.GetDataPresent(format))
            {
                return null;
            }
            return dataObject.GetData(format) as List<TreeNavigationItem>;
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            throw new NotImplementedException();
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            throw new NotImplementedException();
        }
    }
}
