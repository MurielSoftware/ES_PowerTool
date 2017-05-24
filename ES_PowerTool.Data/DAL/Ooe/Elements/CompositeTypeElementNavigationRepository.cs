﻿using Desktop.Data.Core.DAL;
using Desktop.Data.Core.Model;
using Desktop.Shared.Core.Context;
using Desktop.Shared.Core.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ES_PowerTool.Data.DAL.OOE.Elements
{
    public class CompositeTypeElementNavigationRepository : BaseRepository
    {
        public CompositeTypeElementNavigationRepository(Connection connection)
            : base(connection)
        {
        }

        public List<TreeNavigationItem> FindChildren(Guid parentId)
        {
            return GetContext().Set<CompositeTypeElement>()
                .Where(x => x.OwningTypeId == parentId)
                .Select(x => new CompositeTypeElementTreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE_ELEMENT, ProjectId = x.ProjectId, ElementTypeName = x.ElementType.Description })
                .ToList()
                .Cast<TreeNavigationItem>()
                .ToList();
        }

        public TreeNavigationItem FindSpecific(Guid id)
        {
            return GetContext().Set<CompositeTypeElement>()
                .Where(x => x.Id == id)
                .Select(x => new CompositeTypeElementTreeNavigationItem() { Id = x.Id, Name = x.Description, Type = NavigationType.TYPE_ELEMENT, ProjectId = x.ProjectId, ElementTypeName = x.ElementType.Description })
                .SingleOrDefault();
        }
    }
}