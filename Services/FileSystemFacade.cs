using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using labs_RPM.Core;
using labs_RPM.Interfaces;


namespace labs_RPM.Services
{
    public class FileSystemFacade
    {
        private readonly IStorageAdapter _local;
        private readonly IStorageAdapter _cloud;

        public FileSystemFacade(IStorageAdapter local, IStorageAdapter cloud)
        {
            _local = local;
            _cloud = cloud;
        }

        public void BackupFolder(FolderItem folder)
        {
            Console.WriteLine($"\n--- Фасад: Резервное копирование '{folder.Name}' ---");
            long totalSize = folder.GetSize();

            _cloud.Save(folder.Name, "Данные структуры...");

            Console.WriteLine($"Успешно. Передано {totalSize} байт в облако.");
        }
    }
}
