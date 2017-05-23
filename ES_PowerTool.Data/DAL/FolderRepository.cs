using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Desktop.Shared.Core;
using System;

namespace ES_PowerTool.Data.DAL
{
    public class FolderRepository : BaseRepository
    {
        public FolderRepository(Connection connection) 
            : base(connection)
        {
        }

        public List<Folder> GetFoldersToExport(Guid projectId)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }
    }
}
