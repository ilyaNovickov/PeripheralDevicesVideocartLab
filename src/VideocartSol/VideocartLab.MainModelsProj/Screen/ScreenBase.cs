using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.Screen
{
    [Obsolete]
    public abstract class ScreenBase
    {
        private int width;
        private int height;

        private double frequency;
        private double bandwidth;
        private int bitPerPix;

        public int ScreenWidth
        {
            get => width;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                width = value;
            }
        }

        public int ScreenHeight
        {
            get => height;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                height = value;
            }
        }

        public double ScreenFrequency
        {
            get => frequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
            }
        }

        public double Bandwidth
        {
            get => bandwidth;
            set
            {
                ValuesValidator.ValidUnnullObject(value);
                bandwidth = value;
            }
        }

        public int BitPerPixel
        {
            get => bitPerPix;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                bitPerPix = value;
            }
        }
    }
}
