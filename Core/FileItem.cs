using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Interfaces;

namespace labs_RPM.Core
{
    public class FileItem : IFileSystemItem
    {
        public string Name { get; }
        private readonly long _size;

        public FileItem(string name, long size)
        {
            Name = name;
            _size = size;
        }

        public long GetSize() => _size;

        public void Display(int indent)
            => Console.WriteLine($"{new string(' ', indent)}📄 {Name} ({_size} bytes)");
    }
}
