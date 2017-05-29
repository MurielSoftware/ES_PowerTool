using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core;

namespace ES_PowerTool.Data.DAL.OOE.Types
{
    public class CompositeTypeRepository : BaseRepository
    {
        internal CompositeTypeRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<CompositeType> GetCompositeTypesToExport(Guid projectId)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }

        internal bool IsTypeCompositeType(Guid id)
        {
            return GetContext().Set<CompositeType>().Count(x => x.Id == id) > 0;
        }

        internal List<CompositeType> FindCompositeTypesWhereIsUsedTypeInSuperTypes(Guid compositeTypeId)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.SuperTypes.Any(y => y.Id == compositeTypeId))
                .ToList();
        }

        internal List<CompositeType> FindCompositeTypesToFolderIds(ICollection<Guid> folderIds)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => folderIds.Contains(x.FolderId))
                .ToList();
        }

        internal List<Guid> FindCompositeTypeIdsToFolderIds(ICollection<Guid> folderIds)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => folderIds.Contains(x.FolderId))
                .Select(x => x.Id)
                .ToList();
        }

        internal List<CompositeType> FindCompositeTypeOwnerOfCompositeTypeElementsToElementTypeId(Guid typeId)
        {
            return GetContext().Set<CompositeTypeElement>()
                .Where(x => x.ElementTypeId == typeId)
                .Select(x => x.OwningType)
                .ToList();
        }
    }
}
