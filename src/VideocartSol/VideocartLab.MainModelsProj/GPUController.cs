using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.MainModelsProj
{
    /// <summary>
    /// Контроллер GPU
    /// </summary>
    public class GPUController : BaseModel
    {
        private GPU? gpu;
        private VRAM? vram;
        private VideocartLab.MainModelsProj.ConnectionInterface.ConnectionInterface? connectionInterface;
        private ScreenInterface? screenInterface;

        /// <summary>
        /// Цип GPU
        /// </summary>
        public GPU? GPU
        {
            get => gpu;
            set => gpu = value;
        }

        /// <summary>
        /// Видеопамять карты
        /// </summary>
        public VRAM? VRAM
        {
            get => vram;
            set => vram = value;
        }

        /// <summary>
        /// Интерфейс подключения к материнской плате
        /// </summary>
        public ConnectionInterface.ConnectionInterface? ConnectionInterface
        {
            get => connectionInterface;
            set => connectionInterface = value;
        }

        /// <summary>
        /// Интерфейс подключения к монитору
        /// </summary>
        public ScreenInterface? ScreenInterface
        {
            get => screenInterface;
            set => screenInterface = value;
        }

        /// <summary>
        /// Список действий контроллера
        /// </summary>
        public List<GPUActions>? Actions { get; set; }
    }
}
