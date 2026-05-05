using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace labs_RPM
{
    public interface IDocumentState
    {
        void Print(Doc document);
        void AddToQueue(Doc document);

        void CompletePrinting(Doc document);
        void FailPrinting(Doc document);
        void Reset(Doc document);
    }

    public class Doc : Colleague
    {
        public string Name { get; private set; }
        private IDocumentState _state;

        public Doc(string name) : base()
        {
            Name = name;
            _state = new NewState();
        }

        public new IMediator Mediator { get => base.Mediator; set => base.Mediator = value; }



        // Метод для смены состояния
        public void SetState(IDocumentState state) => _state = state;

        // Делегирование поведения текущему состоянию
        public void Print() => _state.Print(this);
        public void AddToQueue() => _state.AddToQueue(this);
        public void CompletePrinting() => _state.CompletePrinting(this);
        public void FailPrinting() => _state.FailPrinting(this);
        public void Reset() => _state.Reset(this);
    }
    
    // Состояние: New (документ создан, можно добавить в очередь)
    public class NewState : IDocumentState
    {
        public void Print(Doc doc)
        {
            // Уведомляем медиатора: документ хочет печататься
            doc.Mediator?.Notify(doc, "RequestPrint", doc);
        }

        public void AddToQueue(Doc doc)
        {
            doc.Mediator?.Notify(doc, "AddedToQueue", doc);
        }

        public void CompletePrinting(Doc doc)
        {
            // Невозможно завершить печать, если документ ещё не печатается
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }

        public void FailPrinting(Doc doc)
        {
            // Невозможно завершить с ошибкой, если документ ещё не печатается
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }

        public void Reset(Doc doc)
        {
            // Документ уже в состоянии New
            doc.Mediator?.Notify(doc, "AlreadyNew", doc);
        }
    }

    // Состояние: Printing (документ печатается)
    public class PrintingState : IDocumentState
    {
        public void Print(Doc doc)
        {
            // Документ уже печатается
            doc.Mediator?.Notify(doc, "AlreadyPrinting", doc);
        }

        public void AddToQueue(Doc doc)
        {
            // Нельзя добавить в очередь — документ уже печатается
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }

        public void CompletePrinting(Doc doc)
        {
            // Успешная печать → переход в Done
            doc.Mediator?.Notify(doc, "PrintingComplete", doc);
        }

        public void FailPrinting(Doc doc)
        {
            // Ошибка печати → переход в Error
            doc.Mediator?.Notify(doc, "PrintingFailed", doc);
        }

        public void Reset(Doc doc)
        {
            // Нельзя сбросить — документ正在 печатается
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }
    }

    // Состояние: Done (документ напечатан)
    public class DoneState : IDocumentState
    {
        public void Print(Doc doc)
        {
            // Нужен сброс перед повторной печатью
            doc.Mediator?.Notify(doc, "NeedReset", doc);
        }

        public void AddToQueue(Doc doc)
        {
            // Нельзя добавить — документ уже напечатан
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }

        public void CompletePrinting(Doc doc)
        {
            // Уже напечатан
            doc.Mediator?.Notify(doc, "AlreadyDone", doc);
        }

        public void FailPrinting(Doc doc)
        {
            // Нельзя завершить с ошибкой — уже напечатан успешно
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }

        public void Reset(Doc doc)
        {
            // Сброс для повторной печати → переход в New
            doc.Mediator?.Notify(doc, "ResetFromDone", doc);
        }
    }

    // Состояние: Error (ошибка печати)
    public class ErrorState : IDocumentState
    {
        public void Print(Doc doc)
        {
            // Нужен сброс перед печатью
            doc.Mediator?.Notify(doc, "NeedReset", doc);
        }

        public void AddToQueue(Doc doc)
        {
            // Нельзя добавить — сначала сброс
            doc.Mediator?.Notify(doc, "NeedReset", doc);
        }

        public void CompletePrinting(Doc doc)
        {
            // Ошибка не устранена
            doc.Mediator?.Notify(doc, "InvalidOperation", doc);
        }

        public void FailPrinting(Doc doc)
        {
            // Уже в состоянии ошибки
            doc.Mediator?.Notify(doc, "AlreadyError", doc);
        }

        public void Reset(Doc doc)
        {
            // Сброс после ошибки → переход в New
            doc.Mediator?.Notify(doc, "ResetFromError", doc);
        }
    }
}