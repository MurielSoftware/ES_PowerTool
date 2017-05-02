using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Events.Publishing
{
    public class PublishEvent
    {
        public EventType EventType { get; private set; }
        public Guid AffectedObjectId { get; private set; }
        public Guid? ParentObjectId { get; private set; }

        public PublishEvent(EventType eventType, Guid affectedObjectId, Guid? parentObjectId)
        {
            EventType = eventType;
            AffectedObjectId = affectedObjectId;
            ParentObjectId = parentObjectId;
        }

        public static PublishEvent CreateCreationEvent(Guid affectedObjectId, Guid? parentObjectId)
        {
            return new PublishEvent(EventType.CREATE, affectedObjectId, parentObjectId);
        }

        public static PublishEvent CreateUpdateEvent(Guid affectedObjectId, Guid? parentObjectId)
        {
            return new PublishEvent(EventType.UPDATE, affectedObjectId, parentObjectId);
        }

        public static PublishEvent CreateDeletionEvent(Guid affectedObjectId, Guid? parentObjectId)
        {
            return new PublishEvent(EventType.DELETE, affectedObjectId, parentObjectId);
        }
    }
}
