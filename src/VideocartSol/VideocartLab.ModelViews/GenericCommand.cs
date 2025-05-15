using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VideocartLab.ModelViews
{
    public class GenericCommand<T> : ICommand
    {
        private Action<T> action;
        private Func<T?, bool>? canExe;

        public GenericCommand(Action<T> action, Func<T?, bool>? canExe = null)
        {
            this.action = action;
            this.canExe = canExe;
        }

        public bool CanExecute(object? parameter)
        {
            return canExe == null || (parameter is T val && canExe(val));
        }

        public bool CanExecute(T? parameter)
        {
            return canExe == null || canExe(parameter);
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object? parameter)
        {
            if (parameter is not T val)
                return;

            action(val);
        }

        public void Execute(T paremeter)
        {
            action(paremeter);
        }
    }
}
