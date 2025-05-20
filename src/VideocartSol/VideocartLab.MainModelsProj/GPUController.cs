using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.ConnectionInterface;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.MainModelsProj
{
    public class GPUController : BaseModel
    {
        private GPU? gpu;
        private VRAM? vram;
        private VideocartLab.MainModelsProj.ConnectionInterface.ConnectionInterface? connectionInterface;
        private ScreenInterface? screenInterface;

        public GPU? GPU
        {
            get => gpu;
            set => gpu = value;
        }

        public VRAM? VRAM
        {
            get => vram;
            set => vram = value;
        }

        public ConnectionInterface.ConnectionInterface? ConnectionInterface
        {
            get => connectionInterface;
            set => connectionInterface = value;
        }

        public ScreenInterface? ScreenInterface
        {
            get => screenInterface;
            set => screenInterface = value;
        }
    }
}
