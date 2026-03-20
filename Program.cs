using labs_RPM.Core;
using labs_RPM.Adapters;
using labs_RPM.Services;

namespace labs_RPM
{
    internal class Program // Лабораторная работа 4
    {
        static void Main(string[] args)
        {
            // 1. Строим структуру (Composite)
            var root = new FolderItem("MyDocuments");
            var work = new FolderItem("Work");
            work.Add(new FileItem("Report.pdf", 1024));
            work.Add(new FileItem("Data.csv", 2048));

            root.Add(work);
            root.Add(new FileItem("Photo.jpg", 5000));

            // 2. Отображаем
            root.Display(0);

            // 3. Используем упрощенный интерфейс (Facade + Adapter)
            var facade = new FileSystemFacade(new LocalStorageAdapter(), new CloudStorageAdapter());
            facade.BackupFolder(root);
        }
    }
}