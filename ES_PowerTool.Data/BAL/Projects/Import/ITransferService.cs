using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.BAL.Projects.Import
{
    public interface ITransferService<T>
    {
        void DoWork(Connection connection, T dto);
        string GetMessage();
    }
}
