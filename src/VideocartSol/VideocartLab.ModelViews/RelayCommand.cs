using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VideocartLab.ModelViews
{
    public class RelayCommand : ICommand
    {
        private Action action;
        private Func<object?, bool>? canExe;

        public RelayCommand(Action action, Func<object?, bool>? canExe = null)
        {
            this.action = action;
            this.canExe = canExe;
        }

        public bool CanExecute(object? parameter)
        {
            return canExe == null || canExe(parameter);
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object? parameter)
        {
            action();
        }
    }
}
