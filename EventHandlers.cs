using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM
{
    // ==================== ОБРАБОТЧИКИ СОБЫТИЙ ====================

    public class AddDocumentHandler : IEventHandler
    {
        private readonly PrintQueue _queue;

        public AddDocumentHandler(PrintQueue queue)
        {
            _queue = queue;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            _queue.EnqueueItem(doc);
        }
    }

    public class EnqueuedHandler : IEventHandler
    {
        private readonly Logger _logger;
        private readonly PrintQueue _queue;

        public EnqueuedHandler(Logger logger, PrintQueue queue)
        {
            _logger = logger;
            _queue = queue;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            _logger.WriteMessage($"Документ '{doc.Name}' добавлен в очередь. В очереди: {_queue.Count}");
        }
    }

    public class ProcessQueueHandler : IEventHandler
    {
        private readonly PrintQueue _queue;
        private readonly Logger _logger;

        public ProcessQueueHandler(PrintQueue queue, Logger logger)
        {
            _queue = queue;
            _logger = logger;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            if (_queue.IsEmpty)
            {
                _logger.WriteMessage("Очередь пуста.");
                return;
            }

            var nextDoc = _queue.DequeueItem();
            nextDoc.SetMediator(((Colleague)sender).Mediator);
            nextDoc.Print();
        }
    }

    public class RequestPrintHandler : IEventHandler
    {
        private readonly Printer _printer;

        public RequestPrintHandler(Printer printer)
        {
            _printer = printer;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            doc.SetState(new PrintingState());
            _printer.StartPrint(doc);
        }
    }

    public class PrintSuccessHandler : IEventHandler
    {
        private readonly Logger _logger;

        public PrintSuccessHandler(Logger logger)
        {
            _logger = logger;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            doc.CompletePrinting();
        }
    }

    public class PrintFailedHandler : IEventHandler
    {
        private readonly Logger _logger;

        public PrintFailedHandler(Logger logger)
        {
            _logger = logger;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            doc.FailPrinting();
        }
    }

    public class PrintingCompleteHandler : IEventHandler
    {
        private readonly Logger _logger;

        public PrintingCompleteHandler(Logger logger)
        {
            _logger = logger;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            doc.SetState(new DoneState());
            _logger.WriteMessage($"Документ '{doc.Name}' напечатан успешно.");
        }
    }

    public class PrintingFailedHandler : IEventHandler
    {
        private readonly Logger _logger;

        public PrintingFailedHandler(Logger logger)
        {
            _logger = logger;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            doc.SetState(new ErrorState());
            _logger.WriteMessage($"Ошибка при печати '{doc.Name}'.");
        }
    }

    public class ResetHandler : IEventHandler
    {
        private readonly Logger _logger;

        public ResetHandler(Logger logger)
        {
            _logger = logger;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            doc.SetState(new NewState());
            _logger.WriteMessage($"Документ '{doc.Name}' сброшен и готов к повторной печати.");
        }
    }

    public class GenericMessageHandler : IEventHandler
    {
        private readonly Logger _logger;
        private readonly string _message;

        public GenericMessageHandler(Logger logger, string message)
        {
            _logger = logger;
            _message = message;
        }

        public void Handle(Colleague sender, Doc doc)
        {
            _logger.WriteMessage($"{_message} '{doc.Name}'.");
        }
    }
}