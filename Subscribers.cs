using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    public class ConsoleSubscriber : NotificationSubscriber
    {
        public ConsoleSubscriber(IMessageFormatter f) : base(f) { }
        protected override void Write(string msg) => Console.WriteLine($"Console: {msg}");
    }

    public class FileSubscriber : NotificationSubscriber
    {
        public FileSubscriber(IMessageFormatter f) : base(f) { }
        protected override void Write(string msg) => File.AppendAllText("log.txt", msg + Environment.NewLine);
    }
}
