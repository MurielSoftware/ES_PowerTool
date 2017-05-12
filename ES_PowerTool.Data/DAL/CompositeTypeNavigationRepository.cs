using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using ES_PowerTool.Data.Model;
using Desktop.Shared.Core.Navigations;

namespace ES_PowerTool.Data.DAL
{
    public class CompositeTypeNavigationRepository : BaseRepository
    {
        public CompositeTypeNavigationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
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
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE })
                .SingleOrDefault();
        }
    }
}
