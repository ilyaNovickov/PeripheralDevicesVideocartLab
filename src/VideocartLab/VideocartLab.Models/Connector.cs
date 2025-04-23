using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VideocartLab.ExtraAbstractions;

namespace VideocartLab.Models
{
    public class Connector : PlaceObject
    {
        private int maxConnectionCount = -1;
        private ObservableCollection<Connector?> targetConnections = new ObservableCollection<Connector?>();
        private Node? parent = null;

        public Node? Parent
        {
            get => parent;
            set
            {
                parent = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Connector?> TargetConnections
        {
            get => targetConnections;
            set
            {
                targetConnections = value;
                OnPropertyChanged();
            }
        }

        public int MaxConnectionCount
        {
            get => maxConnectionCount;
            set
            {
                if (value < -1)
                    throw new Exception($"Значение для свойства {nameof(MaxConnectionCount)} могут быть " +
                        $"распредеелны только в интервале от -1 (максимальное кол-во соединений) до int.MaxValue");

                maxConnectionCount = value;
                OnMaxConnectionCountChanged();
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(MaxConnectionCount))
            {
                OnMaxConnectionCountChanged();
            }
        }

        protected void OnMaxConnectionCountChanged()
        {
            if (MaxConnectionCount <= 0)
                return;

            if (TargetConnections.Count < MaxConnectionCount )
                return;

            List<Connector?> list = TargetConnections.ToList();

            list.RemoveRange(MaxConnectionCount - 1, list.Count - MaxConnectionCount);

            TargetConnections = new ObservableCollection<Connector?>(list);
        }
    }
}
