using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews
{
    
    
    
    public class GPUControllerModelView : ModelViewBase
    {
        private ObservableCollection<GPUAction> actions;

        public GPUControllerModelView()
        {
            InitList();
        }

        public ObservableCollection<GPUAction> GpuActions => actions;

        private void InitList()
        {
            List<GPUAction> actions = new(30);

            actions.Add(new GPUAction(GPUActions.Init, "Иницилизация устройства"));
            //Согласование с экраном
            actions.Add(new GPUAction(GPUActions.HandshakeWithScreenStart, "НАЧАЛО согласования с экраном"));
            actions.Add(new GPUAction(GPUActions.DesicionSolution, "Согласование с монитором разрешения экрана"));
            actions.Add(new GPUAction(GPUActions.DesicionColorDepth, "Согласование с монитором глубины цвета"));
            actions.Add(new GPUAction(GPUActions.DesicionFrameRate, "Согласование с монитором частоты кадров"));
            actions.Add(new GPUAction(GPUActions.HandshakeWithScreenEnd, "КОНЕЦ согласования с экраном"));

            actions.Add(new GPUAction(GPUActions.CPUSentsData, "Прцоессор посылает данные на обработку"));

            //Размещение данных в памяти
            actions.Add(new GPUAction(GPUActions.ControllerPlaceDataInVRAMStart, " НАЧАЛО контроллер GPU размещает данные в памяти VRAM"));
            actions.Add(new GPUAction(GPUActions.PlaceModels, "Размещение информации о моделе (вершинах)"));
            actions.Add(new GPUAction(GPUActions.PlaceTextures, "Размещение текстур"));
            actions.Add(new GPUAction(GPUActions.PlaceSceneInfo, "Размещение информации о сцене"));
            actions.Add(new GPUAction(GPUActions.ReservingPlaceForImage, "Резервирования места под формируемое изображение"));
            actions.Add(new GPUAction(GPUActions.ControllerPlaceDataInVRAMEnd, " КОНЕЦ контроллер GPU размещает данные в памяти VRAM"));
            
            //Обработка данных
            actions.Add(new GPUAction(GPUActions.GPUCalculateDataStart, "НАЧАЛО обработки данных"));
            
            actions.Add(new GPUAction(GPUActions.GPUCalculateDataEnd, "КОНЕЦ обработки данных"));

            actions.Add(new GPUAction(GPUActions.ControllerPlaceImageInVRAM, "Контроллер размещает изображение в памяти VRAM"));
            actions.Add(new GPUAction(GPUActions.ControllerSentImageToScreen, "Отправка изображения на экран"));
            actions.Add(new GPUAction(GPUActions.ControollerFreeDataInVRAM, "Освобождение данных из памяти"));

            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(actions));

            this.actions = new ObservableCollection<GPUAction>(actions);
        }
    }

    public class GPUAction
    {
        public GPUActions Action { get; private set; }
        public string Name { get; private set; }

        public GPUAction(GPUActions action, string name)
        {
            Action = action;
            Name = name;
        }
    }
}
