namespace VideocartLab.MainModelsProj
{
    public class GPU : BaseModel
    {
        private string? name = null;
        private int cores = 1;
        private int tmu = 1;
        private int rop = 1;
        private int frequency = 1;

        /// <summary>
        /// Имя графического процессора GPU
        /// </summary>
        public string? Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Кол-во потоковых ядер (CUDA/SM) [шт]
        /// </summary>
        public int Cores
        {
            get => cores;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                cores = value;
            }
        }

        /// <summary>
        /// Кол-во текстурных блоков [шт]
        /// </summary>
        public int TextureMappingUnits
        {
            get => tmu;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                tmu = value;
            }
        }

        /// <summary>
        /// Кол-во блоков растеризации [шт]
        /// </summary>
        public int RenderOutputPipelines
        {
            get => rop;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                rop = value;
            }
        }

        /// <summary>
        /// Частота GPU [МГц]
        /// </summary>
        public int Frequency
        {
            get => frequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
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
