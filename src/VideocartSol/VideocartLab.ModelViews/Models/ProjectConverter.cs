﻿using VideocartLab.MainModelsProj;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.ModelViews.Models
{
    /// <summary>
    /// Конвертер ViewModel в Model
    /// </summary>
    internal class ProjectConverter
    {
        /// <summary>
        /// Словарь соотношений типов ModelView к функциям конвертации моделей 
        /// </summary>
        private static Dictionary<Type, Func<ModelViewBase, object?>> modelDict;

        static ProjectConverter()
        {
            modelDict = new Dictionary<Type, Func<ModelViewBase, object?>>();

            modelDict.Add(typeof(ConnectionModelView), (vm) =>
            {
                ConnectionModelView? connVM = vm as ConnectionModelView;
                ConnectionModel conn = new ConnectionModel()
                {
                    Id = connVM!.Id,
                    Type = connVM.Type
                };
                return conn;
            });

            #region UsualModels
            modelDict.Add(typeof(VRAMModelView), (vm) =>
            {
                VRAMModelView? vramVM = vm as VRAMModelView;
                VRAM vram = new VRAM()
                {
                    Capacity = vramVM!.Capacity!.Value,
                    Type = vramVM.SelectedGDDR!.Type,
                    MemoryBusCapacity = vramVM.MemoryBusCapacity!.Value,
                    RealFrequency = vramVM.RealFrequency!.Value,
                };
                return vram;
            });
            modelDict.Add(typeof(GPUControllerModelView), (vm) =>
            {
                GPUControllerModelView? controllerVM = vm as GPUControllerModelView;
                GPUController controller = new GPUController();
                controller.Actions = (from action in controllerVM!.GpuActions 
                                      select action.Action).ToList();
                return controller;
            });
            modelDict.Add(typeof(ScreenInterfaceViewModel), (vm) =>
            {
                ScreenInterfaceViewModel? screenVM = vm as ScreenInterfaceViewModel;
                ScreenInterface screenInterface = new ScreenInterface(bandwidth: screenVM!.Bandwidth!.Value,
                    frequency: screenVM!.Frequency!.Value,
                    bitPerPixel: screenVM!.BitPerPixel!.Value,
                    width: screenVM!.ScreenWidth!.Value,
                    height: screenVM!.ScreenHeight!.Value);
                return screenInterface;
            });
            modelDict.Add(typeof(GPUContentModelView), (vm) =>
            {
                GPUContentModelView? gpuVM = vm as GPUContentModelView;
                GPU gpu = new GPU()
                {
                    Cores = gpuVM!.Cores!.Value,
                    Name = gpuVM!.Name,
                    Frequency = gpuVM!.Frequency!.Value,
                    RenderOutputPipelines = gpuVM!.RenderOutputPipelines!.Value,
                    TextureMappingUnits = gpuVM!.TextureMappingUnits!.Value
                };
                return gpu;
            });
            modelDict.Add(typeof(ConnectionInterfaceModelView), (vm) =>
            {
                ConnectionInterfaceModelView? connectionVM = vm as ConnectionInterfaceModelView;

                return connectionVM!.SelectedInterface!.Interface;
            });
            #endregion
        }

        /// <summary>
        /// Конвертация ModelView в Model
        /// </summary>
        /// <param name="modelViewBase">Входное ModelView</param>
        /// <returns>Модель</returns>
        public object? ConvertToModel(ModelViewBase modelViewBase)
        {
            return modelDict[modelViewBase.GetType()].Invoke(modelViewBase);
        }
    }
}
