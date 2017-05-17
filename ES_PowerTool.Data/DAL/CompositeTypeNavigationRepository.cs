using System;
using System.Collections.Generic;
using System.Linq;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeNavigationRepository : BaseRepository
    {
        public CompositeTypeNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        public List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.FolderId == parentId)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE, HasRemoteChildren = x.Children.Count > 0 })
                .ToList();
        }

        public TreeNavigationItem FindSpecific(Guid id)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE, HasRemoteChildren = x.Children.Count > 0 })
                .SingleOrDefault();
        }

        internal List<TreeNavigationItem> FindAllDerivableTypes()
        {
            return GetContext().Set<CompositeType>()
                .AsNoTracking()
                .Where(x => x.Derivable == true)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE })
                .ToList();
        }

        internal List<TreeNavigationItem> FindAllTypes()
        {
            return GetContext().Set<CompositeType>()
                .AsNoTracking()
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE })
                .ToList();
        }
    }
}
