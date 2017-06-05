using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Handlers
{
    public abstract class BaseHandler
    {

        public void Execute(ExecutionEvent executionEvent)
        {
            try
            {
                DoExecute(executionEvent);
            }
            catch(Exception ex)
            {
                OnFailure(executionEvent);
            }
        }

        protected abstract void DoExecute(ExecutionEvent executionEvent);
        protected abstract void OnSuccessful(ExecutionEvent executionEvent, Guid affectedObjectId);
        protected abstract void OnFailure(ExecutionEvent executionEvent);
    }
}