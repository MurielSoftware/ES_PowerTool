using Desktop.Ui.Core.Events.Selection;
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
        private ICollection<IServerChangedListener> _serverListeners = new List<IServerChangedListener>();

        public static Publisher GetInstance()
        {
            if(instance == null)
            {
                instance = new Publisher();
            }
            return instance;
        }

        public void AddListener(IPublishListener listener)
        {
            _listeners.Add(listener);
        }

        public void AddServerListener(IServerChangedListener serverChangedListener)
        {
            _serverListeners.Add(serverChangedListener);
        }

        public void RemoveListener(IPublishListener listener)
        {
            _listeners.Remove(listener);
        }

        public void RemoveServerListener(IServerChangedListener serverChangedListener)
        {
            _serverListeners.Remove(serverChangedListener);
        }

        public void Publish(PublishEvent publishEvent)
        {
            foreach(IPublishListener listener in _listeners)
            {
                listener.OnEvent(publishEvent);
            }
        }

        public void ServerChanged(object obj)
        {
            foreach(IServerChangedListener serverChangedListener in _serverListeners)
            {
                serverChangedListener.OnServerSwitched(obj);
            }
        }
    }
}
