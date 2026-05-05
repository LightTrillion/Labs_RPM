namespace labs_RPM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления очередью печати ===\n");

            // Создаём посредника (он создаст всех коллег внутри)
            var mediator = new PrintSystemMediator();
            var dispatcher = mediator.GetDispatcher();
            var printer = mediator.GetPrinter();

            // Создаём документы
            var doc1 = new Doc("Report.pdf");
            var doc2 = new Doc("Photo.jpg");
            var doc3 = new Doc("Data.xlsx");

            // Устанавливаем посредника документам (чтобы состояния могли уведомлять)
            doc1.SetMediator(mediator);
            doc2.SetMediator(mediator);
            doc3.SetMediator(mediator);

            Console.WriteLine("\n--- СЦЕНАРИЙ 1: Успешная печать ---");
            dispatcher.CommandAddToQueue(doc1);
            dispatcher.CommandAddToQueue(doc2);
            dispatcher.CommandProcessQueue();  // Печать doc1
            dispatcher.CommandProcessQueue();  // Печать doc2

            Console.WriteLine("\n--- СЦЕНАРИЙ 2: Ошибка принтера ---");
            printer.SimulateFailure = true;  // Ломаем принтер
            dispatcher.CommandAddToQueue(doc3);
            dispatcher.CommandProcessQueue();  // Попытка печати → ошибка

            Console.WriteLine("\n--- СЦЕНАРИЙ 3: Восстановление ---");
            doc3.Reset();  // Сбрасываем документ в состояние New
            dispatcher.CommandAddToQueue(doc3);
            dispatcher.CommandProcessQueue();  // Повторная печать

            Console.WriteLine("\n=== Работа завершена ===");
        }
    }
}