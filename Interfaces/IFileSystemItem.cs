using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM.Interfaces
{
    public interface IFileSystemItem
    {
        string Name { get; }
        long GetSize();
        void Display(int indent);
    }
}
