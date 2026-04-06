using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    public abstract class NotificationSubscriber
    {
        private IMessageFormatter _formatter;

        protected NotificationSubscriber(IMessageFormatter formatter) => _formatter = formatter;

        // Шаблонный метод
        public void SendNotification(string eventType, string message)
        {
            string formattedMessage = _formatter.Format(eventType, message);
            Write(formattedMessage);
        }

        protected abstract void Write(string message); // Шаг, который реализуют наследники
    }
}
