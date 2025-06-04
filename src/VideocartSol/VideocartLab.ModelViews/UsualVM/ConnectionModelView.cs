using System.Collections.Immutable;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// СОединение узла
    /// </summary>
    public class ConnectionModelView : ModelViewBase
    {
        private ConnectionType type = ConnectionType.Undef;
        private string? id = "";

        /// <summary>
        /// Тип соединения
        /// </summary>
        public ConnectionType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ID соединения
        /// </summary>
        public string? Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Доступные типы соединений
        /// </summary>
        public ImmutableArray<ConnectionType> AvaibleConnectionTypes { get; } = (new List<ConnectionType>()
        {
            ConnectionType.Undef,
            ConnectionType.Getting,
            ConnectionType.Sending,
            ConnectionType.Duplex
        }).ToImmutableArray<ConnectionType>();
    }
}
