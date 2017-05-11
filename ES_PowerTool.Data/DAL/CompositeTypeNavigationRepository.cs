using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Navigations;
using ES_PowerTool.Data.Model;

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
                .Select(x => new TreeNavigationItem() { Id = x.Id, Label = x.Description, Type = NavigationType.TYPE })
                .ToList();
        }

        public List<TreeNavigationItem> FindSpecific(Guid id)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Label = x.Description, Type = NavigationType.TYPE })
                .ToList();
        }
    }
}
