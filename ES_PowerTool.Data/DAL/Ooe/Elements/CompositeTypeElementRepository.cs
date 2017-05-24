using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;
using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ES_PowerTool.Data.DAL.OOE.Elements
{
    public class CompositeTypeElementRepository : BaseRepository
    {
        public CompositeTypeElementRepository(Connection connection)
            : base(connection)
        {
        }

        internal List<CompositeTypeElement> FindCompositeTypeElementsToExport(Guid projectId)
        {
            return GetContext().Set<CompositeTypeElement>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }

        internal List<CompositeTypeElement> FindCompositeTypeElementsToElementTypeId(Guid typeId)
        {
            return GetContext().Set<CompositeTypeElement>()
                .Where(x => x.ElementTypeId == typeId)
                .ToList();
        }
    }
}
