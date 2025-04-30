using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.Screen
{
    public enum AspectRatio
    {
        _1to1, _5to4, _4to3, _16to9,  _40to27
    }

    public class ScreenInterface
    {
        private int bitPerPixel;
        private double bandwidth;
        private double frequency;
        //private AspectRatio aspectRatio = AspectRatio._16to9;
        private int maxWidth;
        private int maxHeight;

        public ScreenInterface(double bandwidth, double frequency, int bitPerPixel, int width, int height)
        {
            Bandwidth = bandwidth;
            Frequency = frequency;
            BitPerPixel = bitPerPixel;

            ValidMaxScreenResolution(width, height);

            MaxWidth = width;
            MaxHeight = height;
        }

        public double Frequency
        {
            get => frequency;
            private set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
            }
        }

        public double Bandwidth
        {
            get => bandwidth;
            private set
            {
                ValuesValidator.ValidUnnullObject(value);
                bandwidth = value;
            }
        }

        public int BitPerPixel
        {
            get => bitPerPixel;
            private set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                bitPerPixel = value;
            }
        }

        public int MaxWidth
        {
            get => maxWidth;
            private set => maxWidth = value;
        }

        public int MaxHeight
        {
            get => maxHeight;
            private set => maxHeight = value;
        }

        private void ValidMaxScreenResolution(int width, int height)
        {
            double requiredBandwidth = width * height * BitPerPixel * 3d * Frequency / 1024d / 1024d / 1024d;

            if (requiredBandwidth > Bandwidth)
                throw new ArgumentException("Слишком большое разрещеие экрана. Нехватает пропускной способности");
            
        }

        /*
        public AspectRatio AspectRatio
        {
            get => aspectRatio;
            set => aspectRatio = value;
        }

        private void ChangedMaxResolution(bool resolutionChange)
        {

        }

        private void foo()
        {

        }
        */
    }
}
