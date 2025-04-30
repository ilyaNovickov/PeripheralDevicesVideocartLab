using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews
{
    public class ScreenInterfaceViewModel : ModelViewBase
    {
        private int bitPerPixel = 8;
        private double bandwidth = 1;
        private double frequency = 1;
        private int screenWidth = 100;
        private int screenHeight = 100;

        public double? Frequency
        {
            get => frequency;
            set
            { 
                frequency = value ?? 0d;
                OnPropertyChanged();
            }
        }

        public double? Bandwidth
        {
            get => bandwidth;
            set
            {
                bandwidth = value ?? 0d;
                OnPropertyChanged();
            }
        }

        public int? BitPerPixel
        {
            get => bitPerPixel;
            set
            {
                bitPerPixel = value ?? 0;
                OnPropertyChanged();
            }
        }

        public int? ScreenWidth
        {
            get => screenWidth;
            set
            {
                screenWidth = value ?? 0;
                OnPropertyChanged();
            }
        }

        public int? ScreenHeight
        {
            get => screenHeight;
            set
            {
                screenHeight = value ?? 0;
                OnPropertyChanged();
            }
        }

        public double RequiredBandwidth
        {
            get => ScreenWidth!.Value * ScreenHeight!.Value * 
                BitPerPixel!.Value * 3d * Frequency!.Value / 1024d / 1024d / 1024d;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(RequiredBandwidth) || e.PropertyName == nameof(Bandwidth))
                return;

            OnPropertyChanged(nameof(RequiredBandwidth));
        }
    }
}
