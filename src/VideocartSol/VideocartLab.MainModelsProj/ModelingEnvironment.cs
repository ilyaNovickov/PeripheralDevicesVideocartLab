using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj
{
    public enum GPUActions
    {
        Init,
        HandshakeWithScreenStart,
        //Согласование интерфейсов
        HandshakeWithScreenEnd,
        CPUSentsData,
        ControllerPlaceDataInVRAM,
        GPUCalculateDataStart,
        //Обработка вершин и тд
        GPUCalculateDataEnd,
        ControllerPlaceImageInVRAM,
        ControllerSentImageToScreen,
        ControollerFreeDataInVRAM,
    }

    public class ModelingEnvironment 
    {
        public GPUController? GPUController { get; set; }
    }
}
