using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Events.Publishing
{
    public interface IPublishListener
    {
        void OnEvent(PublishEvent publishEvent);
    }
}
