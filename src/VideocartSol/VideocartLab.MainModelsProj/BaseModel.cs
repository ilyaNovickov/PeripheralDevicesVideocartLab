using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.ConnectionInterface;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.MainModelsProj
{
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
