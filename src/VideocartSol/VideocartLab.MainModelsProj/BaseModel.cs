using System.Text.Json.Serialization;
using VideocartLab.MainModelsProj.ConnectionInterface;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.MainModelsProj
{
    //Атрибуты для JSON полиморфной сериализации
    /// <summary>
    /// Базовый класс для моделей
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(VRAM), "vram")]
    [JsonDerivedType(typeof(GPU), "gpu")]
    [JsonDerivedType(typeof(GPUController), "controller")]
    [JsonDerivedType(typeof(VideocartLab.MainModelsProj.ConnectionInterface.ConnectionInterface),
        "connectioninterface")]
    [JsonDerivedType(typeof(AGP), "agp")]
    [JsonDerivedType(typeof(PCI), "pci")]
    [JsonDerivedType(typeof(PCIe), "pcie")]
    [JsonDerivedType(typeof(GDDRType), "gddrtype")]
    [JsonDerivedType(typeof(ScreenInterface), "screeninterface")]
    public class BaseModel
    {
    }
}
