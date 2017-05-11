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
    public class FolderNavigationRepository : BaseRepository
    {
        public FolderNavigationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public List<TreeNavigationItem> FindRoots()
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == null)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Label = x.Name, Type = NavigationType.FOLDER })
                .ToList();
        }

        public List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == parentId)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Label = x.Name, Type = NavigationType.FOLDER })
                .ToList();
        }

        public List<TreeNavigationItem> FindSpecific(Guid id)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Label = x.Name, Type = NavigationType.FOLDER })
                .ToList();
        }
    }
}
