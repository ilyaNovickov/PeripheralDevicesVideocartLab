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
        //Согласование интерфейсов
        HandshakeWithScreenStart,
        DesicionSolution,
        DesicionColorDepth,
        DesicionFrameRate,
        HandshakeWithScreenEnd,

        CPUSentsData,
        //Размещение данных в памяти
        ControllerPlaceDataInVRAMStart,
        PlaceModels,
        PlaceTextures,
        PlaceSceneInfo,
        ReservingPlaceForImage,
        ControllerPlaceDataInVRAMEnd,
        //Обработка вершин и тд
        GPUCalculateDataStart,

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
