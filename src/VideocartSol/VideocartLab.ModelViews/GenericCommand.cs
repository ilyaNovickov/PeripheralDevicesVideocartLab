using System.Windows.Input;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Команда с вызовом дествия с аргументов
    /// </summary>
    /// <typeparam name="T">Тип агрумента, передаваемого команде</typeparam>
    public class GenericCommand<T> : ICommand
    {
        private Action<T> action;
        private Func<T?, bool>? canExe;

        /// <summary>
        /// Иницилизация команды
        /// </summary>
        /// <param name="action">Дествие на выполнение</param>
        /// <param name="canExe">Метод, определяющий может ли выполняться команда</param>
        public GenericCommand(Action<T> action, Func<T?, bool>? canExe = null)
        {
            this.action = action;
            this.canExe = canExe;
        }

        /// <summary>
        /// Определение того, может ли команда выполняться с такими параметрами
        /// </summary>
        /// <param name="parameter">Передаваемые команде параметры</param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return canExe == null || (parameter is T val && canExe(val));
        }

        /// <summary>
        /// Определение того, может ли команда выполняться с такими параметрами
        /// </summary>
        /// <param name="parameter">Передаваемые команде параметры</param>
        /// <returns></returns>
        public bool CanExecute(T? parameter)
        {
            return canExe == null || canExe(parameter);
        }

        /// <summary>
        /// Событие изменения возможности команды выполняться
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <param name="paremeter">Передаваемый параметр</param>
        public void Execute(object? parameter)
        {
            if (parameter is not T val)
                return;

            action(val);
        }

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <param name="paremeter">Передаваемый параметр</param>
        public void Execute(T paremeter)
        {
            action(paremeter);
        }
    }
}
