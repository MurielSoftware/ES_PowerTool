using Desktop.App.Core.Handlers;
using ES_PowerTool.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Handlers
{
    public class GenerateDtoHandler : BaseHandler
    {
        protected override void DoExecute(ExecutionEvent executionEvent)
        {
            DtoClassTemplate ct = new DtoClassTemplate();
            ct.Session = new Dictionary<string, object>();
            ct.Session.Add("_namespace", "xxx");
            ct.Session.Add("_className", "yyy");
            ct.Initialize();
            string x = ct.TransformText();
        }

        protected override void OnFailure(ExecutionEvent executionEvent)
        {
            throw new NotImplementedException();
        }

        protected override void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId)
        {
            throw new NotImplementedException();
        }
    }
}
