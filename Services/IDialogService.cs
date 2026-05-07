namespace laba_9_MVVM.Services
{
    public interface IDialogService
    {
        void ShowInfo(string message);            // Информационное сообщение
        void ShowWarning(string message);         // Предупреждение
        void ShowError(string message);           // Ошибка
        bool ShowConfirmation(string message);    // Запрос Да/Нет, возвращает true при согласии
    }
}