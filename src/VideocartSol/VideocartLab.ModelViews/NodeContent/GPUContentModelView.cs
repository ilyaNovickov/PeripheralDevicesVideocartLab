using System.ComponentModel.DataAnnotations;
using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews
{
    public class GPUContentModelView : ModelViewBase
    {
        private string name = "";
        private int cores = 1;
        private int tmu = 1;
        private int rop = 1;
        private int frequency = 1;

        /// <summary>
        /// Имя графического процессора GPU
        /// </summary>
        public string Name
        {
            get => name;
            set 
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Кол-во потоковых ядер (CUDA/SM) [шт]
        /// </summary>
        public int? Cores
        {
            get => cores;
            set
            {
                cores = value ?? 0;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Кол-во текстурных блоков [шт]
        /// </summary>
        public int? TextureMappingUnits
        {
            get => tmu;
            set
            {
                tmu = value ?? 0;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TextureFillRate));
            }
        }

        /// <summary>
        /// Кол-во блоков растеризации [шт]
        /// </summary>
        public int? RenderOutputPipelines
        {
            get => rop;
            set
            {
                rop = value ?? 0;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PixelFillRate));
            }
        }

        /// <summary>
        /// Частота GPU [МГц]
        /// </summary>
        public int? Frequency
        {
            get => frequency;
            set
            {
                frequency = value ?? 0;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PixelFillRate));
                OnPropertyChanged(nameof(TextureFillRate));
            }
        }

        /// <summary>
        /// Производительность пикселей 
        /// Показывает, сколько пикселей просчитывается за 1 секунду 
        /// [ГПикселей/с]
        /// </summary>
        public double PixelFillRate
        {
            get => rop * frequency / 1000d;
        }

        /// <summary>
        /// Текстурная производительность
        /// Показывает кол-во текстелей, которые обрабатываются за 1 секунду
        /// [ГТекстелей/с]
        /// </summary>
        public double TextureFillRate
        {
            get => frequency * tmu / 1000d;
        }
    }
}
