using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    // Интерфейс посредника
    public interface IMediator
    {
        void Notify(Colleague sender, string eventName, Doc document = null);
    }

    // Интерфейс обработчика событий
    public interface IEventHandler
    {
        void Handle(Colleague sender, Doc doc);
    }

    // Базовый класс для всех коллег
    public abstract class Colleague
    {
        public IMediator Mediator { get; protected set; }

        public void SetMediator(IMediator mediator)
        {
            Mediator = mediator;
        }
    }

    // КОНКРЕТНЫЕ КОЛЛЕГИ

    public class Printer : Colleague
    {
        public bool SimulateFailure { get; set; } = false;

        public void StartPrint(Doc doc)
        {
            Console.WriteLine($"[Printer] Печать документа '{doc.Name}'...");

            if (SimulateFailure)
            {
                SimulateFailure = false;
                Mediator.Notify(this, "PrintFailed", doc);
            }
            else
            {
                Mediator.Notify(this, "PrintSuccess", doc);
            }
        }
    }

    public class PrintQueue : Colleague
    {
        private Queue<Doc> _queue = new Queue<Doc>();

        public void EnqueueItem(Doc doc)
        {
            _queue.Enqueue(doc);
            Mediator.Notify(this, "Enqueued", doc);
        }

        public Doc DequeueItem()
        {
            if (_queue.Count > 0)
                return _queue.Dequeue();
            return null;
        }

        public bool IsEmpty => _queue.Count == 0;
        public int Count => _queue.Count;
    }

    public class Logger : Colleague
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    public class Dispatcher : Colleague
    {
        public void CommandProcessQueue()
        {
            Mediator.Notify(this, "ProcessQueue");
        }

        public void CommandAddToQueue(Doc doc)
        {
            Mediator.Notify(this, "AddDocument", doc);
        }
    }



    // ПОСРЕДНИК

    public class PrintSystemMediator : IMediator
    {
        private readonly Printer _printer;
        private readonly PrintQueue _queue;
        private readonly Logger _logger;
        private readonly Dispatcher _dispatcher;
        private readonly Dictionary<string, IEventHandler> _handlers;

        public PrintSystemMediator()
        {
            _printer = new Printer();
            _queue = new PrintQueue();
            _logger = new Logger();
            _dispatcher = new Dispatcher();

            // Устанавливаем посредника всем коллегам
            _printer.SetMediator(this);
            _queue.SetMediator(this);
            _logger.SetMediator(this);
            _dispatcher.SetMediator(this);

            // Регистрируем обработчики событий
            _handlers = new Dictionary<string, IEventHandler>
            {
                { "AddDocument", new AddDocumentHandler(_queue) },
                { "Enqueued", new EnqueuedHandler(_logger, _queue) },
                { "ProcessQueue", new ProcessQueueHandler(_queue, _logger) },
                { "RequestPrint", new RequestPrintHandler(_printer) },
                { "PrintSuccess", new PrintSuccessHandler(_logger) },
                { "PrintFailed", new PrintFailedHandler(_logger) },
                { "PrintingComplete", new PrintingCompleteHandler(_logger) },
                { "PrintingFailed", new PrintingFailedHandler(_logger) },
                { "ResetFromDone", new ResetHandler(_logger) },
                { "ResetFromError", new ResetHandler(_logger) },
                { "AlreadyNew", new GenericMessageHandler(_logger, "Документ уже в состоянии New") },
                { "AlreadyPrinting", new GenericMessageHandler(_logger, "Документ уже печатается") },
                { "AlreadyDone", new GenericMessageHandler(_logger, "Документ уже напечатан") },
                { "AlreadyError", new GenericMessageHandler(_logger, "Документ уже в состоянии ошибки") },
                { "NeedReset", new GenericMessageHandler(_logger, "Документ требует сброса (Reset) перед операцией") },
                { "InvalidOperation", new GenericMessageHandler(_logger, "Недопустимая операция для документа") }
            };
        }

        public void Notify(Colleague sender, string eventName, Doc doc = null)
        {
            if (_handlers.TryGetValue(eventName, out var handler))
            {
                handler.Handle(sender, doc);
            }
            else
            {
                _logger.WriteMessage($"!!! Неизвестное событие: {eventName}");
            }
        }

        // Методы для демонстрации
        public Printer GetPrinter() => _printer;
        public Dispatcher GetDispatcher() => _dispatcher;
        public Logger GetLogger() => _logger;
    }
}