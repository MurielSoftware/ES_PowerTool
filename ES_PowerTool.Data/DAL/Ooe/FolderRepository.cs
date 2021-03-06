﻿using Desktop.Shared.Core.Context;
using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using System.Collections.Generic;
using System.Linq;
using Desktop.Shared.Core;
using System;

namespace ES_PowerTool.Data.DAL.OOE
{
    public class FolderRepository : BaseRepository
    {
        internal FolderRepository(Connection connection) 
            : base(connection)
        {
        }

        internal List<Folder> FindFoldersToExport(Guid projectId)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ProjectId == projectId && x.State == State.NEW)
                .ToList();
        }

        internal List<Guid> FindAllSubFolderIds(Guid parentFolderId)
        {

            List<Guid> subFolderIds = new List<Guid>();
            foreach(Guid subFolderId in FindDirectlySubFolderIds(parentFolderId))
            {
                subFolderIds.AddRange(FindAllSubFolderIds(subFolderId));
            }
            subFolderIds.Add(parentFolderId);
            return subFolderIds;
        }

        internal List<Guid> FindDirectlySubFolderIds(Guid parentFolderId)
        {
            return GetContext().Set<Folder>()
                .Where(x => x.ParentId == parentFolderId)
                .Select(x => x.Id)
                .ToList();
        }
    }
}
