namespace VideocartLab.MainModelsProj.Screen
{
    /// <summary>
    /// Интерфес подключения к экрану
    /// </summary>
    public class ScreenInterface : BaseModel
    {
        private int bitPerPixel;
        private double bandwidth;
        private double frequency;
        private int maxWidth;
        private int maxHeight;

        /// <summary>
        /// Иницилизация интерфейса подключения к экрану
        /// </summary>
        /// <param name="bandwidth">Пропусаная способность порта [Гбит/с]</param>
        /// <param name="frequency">Частота обновления экран [Гц]</param>
        /// <param name="bitPerPixel">Глубина цвета [бит]</param>
        /// <param name="width">Ширина экрана</param>
        /// <param name="height">Высота экрана</param>
        public ScreenInterface(double bandwidth, double frequency, int bitPerPixel, int width, int height)
        {
            Bandwidth = bandwidth;
            Frequency = frequency;
            BitPerPixel = bitPerPixel;

            ValidMaxScreenResolution(width, height);

            MaxWidth = width;
            MaxHeight = height;
        }

        /// <summary>
        /// Частота обновления экрана [Гц]
        /// </summary>
        public double Frequency
        {
            get => frequency;
            private set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
            }
        }

        /// <summary>
        /// Требуемая пропускная способность [Гбит/с]
        /// </summary>
        public double Bandwidth
        {
            get => bandwidth;
            private set
            {
                ValuesValidator.ValidUnnullObject(value);
                bandwidth = value;
            }
        }

        /// <summary>
        /// Глубина цвета [бит]
        /// </summary>
        public int BitPerPixel
        {
            get => bitPerPixel;
            private set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                bitPerPixel = value;
            }
        }

        /// <summary>
        /// Максимальная ширина экрана
        /// </summary>
        public int MaxWidth
        {
            get => maxWidth;
            private set => maxWidth = value;
        }

        /// <summary>
        /// Максимальная высота экрана
        /// </summary>
        public int MaxHeight
        {
            get => maxHeight;
            private set => maxHeight = value;
        }

        /// <summary>
        /// Определение требуемой пропускной способности с введнной
        /// </summary>
        /// <param name="width">Ширина экрана</param>
        /// <param name="height">Высота экрана</param>
        /// <exception cref="ArgumentException">Слишком большое разрешение экрана</exception>
        private void ValidMaxScreenResolution(int width, int height)
        {
            double requiredBandwidth = width * height * BitPerPixel * 3d * Frequency / 1024d / 1024d / 1024d;

            if (requiredBandwidth > Bandwidth)
                throw new ArgumentException("Слишком большое разрещеие экрана. Нехватает пропускной способности");

        }
    }
}
