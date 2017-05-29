using System;
using System.Collections.Generic;
using System.Linq;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.DAL.OOE.Types
{
    public class CompositeTypeNavigationRepository : BaseRepository
    {
        internal CompositeTypeNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.FolderId == parentId)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.COMPOSITE_TYPE, ProjectId = x.ProjectId, HasRemoteChildren = x.Children.Count > 0, BuiltIn = x.BuiltIn })
                .ToList();
        }

        internal TreeNavigationItem FindSpecific(Guid id)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.COMPOSITE_TYPE, ProjectId = x.ProjectId, HasRemoteChildren = x.Children.Count > 0, BuiltIn = x.BuiltIn })
                .SingleOrDefault();
        }

        internal List<TreeNavigationItem> FindAllDerivableTypes()
        {
            return GetContext().Set<CompositeType>()
                .AsNoTracking()
                .Where(x => x.Derivable == true)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.COMPOSITE_TYPE, ProjectId = x.ProjectId })
                .ToList();
        }

        internal List<TreeNavigationItem> FindAllTypes()
        {
            return GetContext().Set<CompositeType>()
                .AsNoTracking()
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.COMPOSITE_TYPE, ProjectId = x.ProjectId })
                .ToList();
        }
    }
}
