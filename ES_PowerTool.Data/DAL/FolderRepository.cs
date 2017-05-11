using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;

namespace ES_PowerTool.Data.DAL
{
    public class FolderRepository : BaseRepository
    {
        public FolderRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
