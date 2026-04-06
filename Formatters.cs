using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    public class TextFormatter : IMessageFormatter
    {
        public string Format(string type, string msg) => $"[{type}] {msg}";
    }

    public class JsonFormatter : IMessageFormatter
    {
        public string Format(string type, string msg) => $"{{\"event\": \"{type}\", \"message\": \"{msg}\"}}";
    }
}
