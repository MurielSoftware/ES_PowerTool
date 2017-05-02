using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Handlers
{
    public abstract class BaseHandler
    {
        public void Execute()
        {
            try
            {
                DoExecute();
                OnSuccessful();
            }
            catch(Exception ex)
            {
                OnFailure();
            }
        }

        protected abstract void DoExecute();
        protected abstract void OnSuccessful();
        protected abstract void OnFailure();
    }
}
