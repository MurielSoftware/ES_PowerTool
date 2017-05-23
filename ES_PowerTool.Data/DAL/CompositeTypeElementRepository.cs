using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeElementRepository : BaseRepository
    {
        public CompositeTypeElementRepository(Connection connection)
            : base(connection)
        {
        }

        public List<CompositeTypeElement> GetCompositeTypeElementsToExport(Guid projectId)
        {
            return GetContext().Set<CompositeTypeElement>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }
    }
}
