using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Ui.Core.Events.Publishing
{
    public class Publisher
    {
        private static Publisher instance;
        private ICollection<IPublishListener> _listeners = new List<IPublishListener>();

        public static Publisher GetInstance()
        {
            if (instance == null)
            {
                instance = new Publisher();
            }
            return instance;
        }

        public void AddListener(IPublishListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IPublishListener listener)
        {
            _listeners.Remove(listener);
        }

        public void Publish(PublishEvent publishEvent)
        {
            foreach (IPublishListener listener in _listeners)
            {
                listener.OnEvent(publishEvent);
            }
        }
    }
}
