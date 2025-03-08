using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Models.ConnectionInterface
{
    public class PCIe : ConnectionInterface
    {
        public static PCIe PCIe6dot0x16 => new PCIe(16, 32000, 2, EncodingType._242On256);
        public static PCIe PCIe4dot0x8 => new PCIe(8, 16000, 1, EncodingType._128On130b);
        public static PCIe PCIe2dot0x4 => new PCIe(2, 5000, 1, EncodingType._8bOn10b);

        private double frequency = 2500;
        private int lines = 1;
        private double encodingType = 8d / 10d;
        private EncodingType type = EncodingType._8bOn10b;
        private int bitPerClock = 1;

        public PCIe()
        {

        }

        public PCIe(int countofLines, double frequency, int bitPerClock, double encodingType) : 
            this(countofLines, frequency, bitPerClock)
        {
            UserEncodingType = encodingType;
            Type = EncodingType.Another;
        }

        public PCIe(int countofLines, double frequency, int bitPerClock, EncodingType encodingType) :
            this(countofLines, frequency, bitPerClock)
        {
            Type = encodingType;
        }

        private PCIe(int countofLines, double frequency, int bitPerClock)
        {
            BitPerClock = bitPerClock;
            Frequency = frequency;
            Lines = countofLines;
        }

        private int BitPerClock
        {
            get => bitPerClock;
            set
            {
                if (value <= 0)
                    throw new Exception("This value can't be negative or zero");
                bitPerClock = value;
            }
        }

        public EncodingType Type
        {
            get => type;
            set => type = value;
        }

        public double Frequency
        {
            get => frequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
            }
        }

        public int Lines
        {
            get => lines;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                lines = value;
            }
        }

        public override double Bandwidth
        {
            get
            {
                return Lines * MatchEncodingType(Type) * BitPerClock * Frequency / 8d / 1000d;
            }
        }

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
