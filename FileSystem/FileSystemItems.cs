using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Interfaces;

namespace labs_RPM.FileSystem
{
    public class FileItem : IFileSystemItem
    {
        public string Name { get; }
        private long _size;
        public FileItem(string name, long size) { Name = name; _size = size; }
        public long GetSize() => _size;
        public void Display(int indent) => Console.WriteLine(new string(' ', indent) + "📄 " + Name);
    }

    public class FolderItem : IFileSystemItem
    {
        public string Name { get; }
        private List<IFileSystemItem> _children = new();
        public FolderItem(string name) => Name = name;

        public void Add(IFileSystemItem item) => _children.Add(item);
        public long GetSize() => _children.Sum(c => c.GetSize());

        public void Display(int indent)
        {
            Console.WriteLine(new string(' ', indent) + "📁 " + Name);
            foreach (var child in _children) child.Display(indent + 2);
        }
    }

}
