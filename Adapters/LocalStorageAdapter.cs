using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Interfaces;

namespace labs_RPM.Adapters
{
    public class LocalStorageAdapter : IStorageAdapter
    {
        public void Save(string path, string content) => Console.WriteLine($"[Local] Файл {path} записан.");
        public void Delete(string path) => Console.WriteLine($"[Local] Файл {path} удален.");
    }
}
