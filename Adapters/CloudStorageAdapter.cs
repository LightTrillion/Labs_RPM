using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Interfaces;

namespace labs_RPM.Adapters
{
    public class CloudStorageAdapter : IStorageAdapter
    {
        public void Save(string path, string content) => Console.WriteLine($"[Cloud] API: Отправка {path} в облако...");
        public void Delete(string path) => Console.WriteLine($"[Cloud] API: Удаление {path} из облака...");
    }
}
