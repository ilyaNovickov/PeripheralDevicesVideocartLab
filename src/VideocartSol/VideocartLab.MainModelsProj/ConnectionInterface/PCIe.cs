using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.ConnectionInterface
{

    /// <summary>
    /// Интерфейс подключения PCIexpress
    /// </summary>
    public class PCIe : ConnectionInterface
    {
        /// <summary>
        /// Экземпляр PCEe6.0x16
        /// </summary>
        public static PCIe PCIe6dot0x16 => new PCIe(16, 32000, 2, EncodingType._242On256);
        /// <summary>
        /// Экземпляр PCEe4.0x8
        /// </summary>
        public static PCIe PCIe4dot0x8 => new PCIe(8, 16000, 1, EncodingType._128On130b);
        /// <summary>
        /// Экземпляр PCE2.0x4
        /// </summary>
        public static PCIe PCIe2dot0x4 => new PCIe(4, 5000, 1, EncodingType._8bOn10b);

        private double frequency = 2500;
        private int lines = 1;
        private double encodingType = 8d / 10d;
        private EncodingType type = EncodingType._8bOn10b;
        private int bitPerClock = 1;

        /// <summary>
        /// Иницилизация PCIe1.0x1
        /// </summary>
        public PCIe()
        {

        }

        /// <summary>
        /// Иницилизация интерфейса PCIe с пользовательской кодировкой
        /// </summary>
        /// <param name="countofLines">Кол-во линий передачи [шт]</param>
        /// <param name="frequency">Частота [МГц]</param>
        /// <param name="bitPerClock">Кол-во бит, передаваемых за 1-он такт</param>
        /// <param name="encodingType">Пользовательское значение кодировки, которое меньше 1, но больше 0</param>
        public PCIe(int countofLines, double frequency, int bitPerClock, double encodingType) :
            this(countofLines, frequency, bitPerClock)
        {
            UserEncodingType = encodingType;
            Type = EncodingType.Another;
        }

        /// <summary>
        /// Иницилизация интерфейса PCIe 
        /// </summary>
        /// <param name="countofLines">Кол-во линий передачи [шт]</param>
        /// <param name="frequency">Частота [МГц]</param>
        /// <param name="bitPerClock">Кол-во бит, передаваемых за 1-он такт</param>
        /// <param name="encodingType">Тип кодировки данных</param>
        public PCIe(int countofLines, double frequency, int bitPerClock, EncodingType encodingType) :
            this(countofLines, frequency, bitPerClock)
        {
            Type = encodingType;
        }

        /// <summary>
        /// Иницилизация PCIe
        /// </summary>
        /// <param name="countofLines">Кол-во линий передачи [шт</param>
        /// <param name="frequency">Частота [МГц</param>
        /// <param name="bitPerClock">Кол-во бит, передаваемых за 1-он такт</param>
        private PCIe(int countofLines, double frequency, int bitPerClock)
        {
            BitPerClock = bitPerClock;
            Frequency = frequency;
            Lines = countofLines;
        }

        /// <summary>
        /// Кол-во бит передаваемых за 1-н такт
        /// </summary>
        public int BitPerClock
        {
            get => bitPerClock;
            set
            {
                if (value <= 0)
                    throw new Exception("This value can't be negative or zero");
                bitPerClock = value;
            }
        }

        /// <summary>
        /// Тип кодирования передаваемых данных
        /// </summary>
        public EncodingType Type
        {
            get => type;
            set => type = value;
        }

        /// <summary>
        /// Частота работы интерфейса [МГц]
        /// </summary>
        public double Frequency
        {
            get => frequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
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
                ValuesValidator.ValidUnnegativeArgument(value);
                lines = value;
            }
        }

        /// <summary>
        /// Пропускная способность [ГБ/с]
        /// </summary>
        public override double Bandwidth
        {
            get
            {
                return Lines * MatchEncodingType(Type) * BitPerClock * Frequency / 8d / 1000d;
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
