using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.GPUMemory
{
    /// <summary>
    /// Класс типа GDDR памяти
    /// </summary>
    public class GDDRType : BaseModel
    {
        /// <summary>
        /// Создание экземпляра памяти типа GDDR
        /// </summary>
        public static GDDRType GDDR => new GDDRType(GDDRTypes.GDDR);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR2
        /// </summary>
        public static GDDRType GDDR2 => new GDDRType(GDDRTypes.GDDR2);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR3
        /// </summary>
        public static GDDRType GDDR3 => new GDDRType(GDDRTypes.GDDR3);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR4
        /// </summary>
        public static GDDRType GDDR4 => new GDDRType(GDDRTypes.GDDR4);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR5
        /// </summary>
        public static GDDRType GDDR5 => new GDDRType(GDDRTypes.GDDR5);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR5X
        /// </summary>
        public static GDDRType GDDR5X => new GDDRType(GDDRTypes.GDDR5X);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR6
        /// </summary>
        public static GDDRType GDDR6 => new GDDRType(GDDRTypes.GDDR6);
        /// <summary>
        /// Создание экземпляра памяти типа GDDR6X
        /// </summary>
        public static GDDRType GDDR6X => new GDDRType(GDDRTypes.GDDR6X);

        private GDDRTypes type;
        private int fromRealToEffectivFrequency;

        public GDDRType(GDDRTypes type)
        {
            Type = type;
        }

        /// <summary>
        /// Тип GDDR памяти
        /// </summary>
        public GDDRTypes Type
        {
            get => type;
            set
            {
                type = value;
                OnTypeChanged();
            }
        }

        /// <summary>
        /// Коофициент, показывающий отношение эффективной частоты к реальной
        /// </summary>
        public int EffectiveRatio => fromRealToEffectivFrequency;

        /// <summary>
        /// Коофициет, показывающий отношение реальной частоты к эффективной
        /// (велечина обратная EffectiveRatio)
        /// </summary>
        public double RealRatio => 1d / fromRealToEffectivFrequency;

        /// <summary>
        /// Изменение отношения эффективной частоты к реальной при 
        /// изменении типа GDDR памяти
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void OnTypeChanged()
        {
            switch (Type)
            {
                case GDDRTypes.GDDR:
                case GDDRTypes.GDDR2:
                    fromRealToEffectivFrequency = 1;
                    break;
                case GDDRTypes.GDDR3:
                case GDDRTypes.GDDR4:
                    fromRealToEffectivFrequency = 2;
                    break;
                case GDDRTypes.GDDR5:
                    fromRealToEffectivFrequency = 4;
                    break;
                case GDDRTypes.GDDR5X:
                case GDDRTypes.GDDR6:
                    fromRealToEffectivFrequency = 8;
                    break;
                case GDDRTypes.GDDR6X:
                    fromRealToEffectivFrequency = 16;
                    break;
                default:
                    throw new Exception("Unsupported GDDR type");
                    break;
            }
        }
    }
}
