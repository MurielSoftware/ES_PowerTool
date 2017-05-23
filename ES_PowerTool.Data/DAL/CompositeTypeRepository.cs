using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeRepository : BaseRepository
    {
        public CompositeTypeRepository(Connection connection) 
            : base(connection)
        {
        }

        public List<CompositeType> GetCompositeTypesToExport(Guid projectId)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }
    }
}
