using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    public class SystemMonitor
    {
        private readonly List<NotificationSubscriber> _subscribers = new();

        public void Subscribe(NotificationSubscriber s) => _subscribers.Add(s);
        public void Unsubscribe(NotificationSubscriber s) => _subscribers.Remove(s);

        public void Notify(string eventType, string message)
        {
            foreach (var sub in _subscribers)
                sub.SendNotification(eventType, message);
        }
    }
}
