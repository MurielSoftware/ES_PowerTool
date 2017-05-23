﻿using Desktop.Data.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using Desktop.Data.Core.Model;

namespace ES_PowerTool.Data.DAL
{
    public class PrimitiveTypeNavigationRepository : BaseRepository
    {
        public PrimitiveTypeNavigationRepository(Connection connection) 
            : base(connection)
        {
        }

        public List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<PrimitiveType>()
                .Where(x => x.FolderId == parentId)
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.PRIMITIVE_TYPE, ProjectId = x.ProjectId })
                .ToList();
        }

        internal List<TreeNavigationItem> FindAllTypes()
        {
            return GetContext().Set<PrimitiveType>()
                .AsNoTracking()
                .Select(x => new TreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.PRIMITIVE_TYPE, ProjectId = x.ProjectId })
                .ToList();
        }
    }
}
