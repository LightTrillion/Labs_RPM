namespace labs_RPM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var monitor = new SystemMonitor();

            // Создаем подписчиков с разными стратегиями форматирования
            var admin = new ConsoleSubscriber(new TextFormatter());
            var logger = new FileSubscriber(new JsonFormatter());

            monitor.Subscribe(admin);
            monitor.Subscribe(logger);

            // Имитация критических событий
            monitor.Notify("CPU", "Загрузка 95%!");
            monitor.Notify("Network", "Потеря пакетов 20%");
        }
    }
}