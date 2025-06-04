using System.Windows.Input;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Класс команды для простого действия
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action action;
        private Func<object?, bool>? canExe;

        /// <summary>
        /// Иницилизация команды
        /// </summary>
        /// <param name="action">Дествие</param>
        /// <param name="canExe">Метод определения возможности выполнения команды</param>
        public RelayCommand(Action action, Func<object?, bool>? canExe = null)
        {
            this.action = action;
            this.canExe = canExe;
        }

        /// <summary>
        /// Может ли команда выполняться
        /// </summary>
        /// <param name="parameter">Введённые параметры</param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return canExe == null || canExe(parameter);
        }

        /// <summary>
        /// Изменение возможности выполнения команды
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <param name="parameter">Введённые параметры</param>
        public void Execute(object? parameter)
        {
            action();
        }
    }
}
