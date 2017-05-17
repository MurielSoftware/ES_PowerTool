using System;
using System.Collections.Generic;
using System.Linq;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.DAL
{
    public class FolderNavigationRepository : BaseRepository
    {
        public FolderNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<TreeNavigationItem> FindRoots(Guid projectId)
        { 
            return GetContext().Set<Folder>()
                .Where(x => x.ProjectId == projectId && x.ParentId == null)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.FOLDER, ProjectId = x.ProjectId, HasRemoteChildren = x.ModelObjectTypes.Count > 0 || x.Folders.Count > 0 })
                .ToList();
        }

        internal List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == parentId)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.FOLDER, HasRemoteChildren = x.ModelObjectTypes.Count > 0 || x.Folders.Count > 0 })
                .ToList();
        }

        internal TreeNavigationItem FindSpecific(Guid id)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.FOLDER, HasRemoteChildren = x.ModelObjectTypes.Count > 0 || x.Folders.Count > 0 })
                .SingleOrDefault();
        }

        //internal bool ContainsAnyChildren(Guid id)
        //{
        //    return ContainsAnyFolder(id) || ContainsAnyType(id);
        //}

        //internal bool ContainsAnyFolder(Guid id)
        //{
        //    return GetContext().Set<Folder>()
        //        .Where(x => x.ParentId == id)
        //        .Count() > 0;
        //}

        //internal bool ContainsAnyType(Guid id)
        //{
        //    return GetContext().Set<CompositeType>()
        //        .Where(x => x.FolderId == id)
        //        .Count() > 0;
        //}
    }
}
