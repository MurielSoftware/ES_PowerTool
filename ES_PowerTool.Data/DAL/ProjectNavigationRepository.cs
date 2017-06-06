using Desktop.Data.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Navigations;
using Desktop.Shared.Core;

namespace ES_PowerTool.Data.DAL
{
    public class ProjectNavigationRepository : BaseRepository
    {
        internal ProjectNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<TreeNavigationItem> FindRoots()
        {
            return GetContext().Set<Project>()
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PROJECT, ProjectId = x.Id, State = State.NEW, HasRemoteChildren = x.Folders.Count > 0 })
                .ToList();
        }

        internal TreeNavigationItem FindSpecific(Guid id)
        {
            return GetContext().Set<Project>()
                .Where(x => x.Id == id)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Name, Type = NavigationType.PROJECT, ProjectId = x.Id, State = State.NEW, HasRemoteChildren = x.Folders.Count > 0 })
                .SingleOrDefault();
        }
    }
}
