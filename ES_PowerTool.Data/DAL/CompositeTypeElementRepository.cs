using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeElementRepository : BaseRepository
    {
        public CompositeTypeElementRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
