using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    public interface IMessageFormatter
    {
        string Format(string eventType, string message);
    }
}
