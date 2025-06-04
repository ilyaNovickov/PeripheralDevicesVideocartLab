using VideocartLab.MainModelsProj.ConnectionInterface;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Интерфейс типа PCIe
    /// </summary>
    public class PCIExpressViewModel : ModelViewBase
    {
        private double frequency = 2500;
        private int lines = 1;
        private double encodingType = 8d / 10d;
        private EncodingTypeInfo type = new EncodingTypeInfo("8/10", EncodingType._8bOn10b);
        private int bitPerClock = 1;

        /// <summary>
        /// Кол-во бит передаваемых за 1-н такт
        /// </summary>
        public int BitPerClock
        {
            get => bitPerClock;
            set
            {
                bitPerClock = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }

        /// <summary>
        /// Тип кодирования передаваемых данных
        /// </summary>
        public EncodingTypeInfo Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }

        /// <summary>
        /// Частота работы интерфейса [МГц]
        /// </summary>
        public double Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }

        /// <summary>
        /// Кол-во линий передачи [шт]
        /// </summary>
        public int Lines
        {
            get => lines;
            set
            {
                lines = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }

        /// <summary>
        /// Пропускная способность [ГБ/с]
        /// </summary>
        public double Bandwidth
        {
            get
            {
                return Lines * MatchEncodingType(Type.EncodingType) * BitPerClock * Frequency / 8d / 1000d;
            }
        }

        /// <summary>
        /// Пользовательский коэффициент передачи в пределах от 0 (строго больше) до 1 (включительно)
        /// </summary>
        public double UserEncodingType
        {
            get => encodingType;
            set
            {
                if (value <= 0 || value > 1d)
                    throw new Exception("User's encoding value has to be in range [0, 1]");

                encodingType = value;
            }
        }

        /// <summary>
        /// Сопоставление выбранного типа кодирования
        /// </summary>
        /// <param name="type">Тип кодирования</param>
        /// <returns>Значение от 0 до 1, обозначаещее коэффициент полезной длины сообщения</returns>
        /// <exception cref="Exception">Введён неизместный тип кодирования</exception>
        private double MatchEncodingType(EncodingType type)
        {
            switch (type)
            {
                case EncodingType.Another:
                    return encodingType;
                case EncodingType._8bOn10b:
                    return 8d / 10d;
                case EncodingType._64On66:
                    return 64d / 66d;
                case EncodingType._128On130b:
                    return 128d / 130d;
                case EncodingType._242On256:
                    return 242d / 256d;
                default:
                    throw new Exception("Unknown encoding type");
            }
        }
    }
}
