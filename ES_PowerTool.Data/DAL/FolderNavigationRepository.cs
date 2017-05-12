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
    public class FolderNavigationRepository : BaseRepository
    {
        public FolderNavigationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        internal List<TreeNavigationItem> FindRoots()
        { 
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == null)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.FOLDER, HasRemoteChildren = x.CompositeTypes.Count > 0 || x.Folders.Count > 0 })
                .ToList();
        }

        internal List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == parentId)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.FOLDER, HasRemoteChildren = x.CompositeTypes.Count > 0 || x.Folders.Count > 0 })
                .ToList();
        }

        internal TreeNavigationItem FindSpecific(Guid id)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.FOLDER })
                .SingleOrDefault();
        }

        internal bool ContainsAnyChildren(Guid id)
        {
            return ContainsAnyFolder(id) || ContainsAnyType(id);
        }

        internal bool ContainsAnyFolder(Guid id)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == id)
                .Count() > 0;
        }

        internal bool ContainsAnyType(Guid id)
        {
            return GetContext().Set<CompositeType>()
                .Where(x => x.FolderId == id)
                .Count() > 0;
        }
    }
}
