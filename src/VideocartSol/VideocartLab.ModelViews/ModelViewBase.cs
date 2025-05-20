using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Базовый класс для ModelView
    /// </summary>
    public class ModelViewBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызов события изменения свойстйва
        /// </summary>
        /// <param name="name">Имя свойства</param>
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Вызов события изменения своствай
        /// </summary>
        /// <param name="e">Аргументы события изменения свойства</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
