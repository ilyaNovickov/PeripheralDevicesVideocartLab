using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Models
{
    public class GPU
    {
        private string? name = null;
        private int cores = 1;
        private int tmu = 1;
        private int rop = 1;
        private int frequency = 1;


        public string? Name
        {
            get => name;
            set => name = value;
        }

        public int Cores
        {
            get => cores;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                cores = value;
            }
        }

        public int TextureMappingUnits
        {
            get => tmu;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                tmu = value;
            }
        }

        public int RenderOutputPipelines
        {
            get => rop;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                rop = value;
            }
        }

        public int Frequency
        {
            get => frequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
            }
        }
    }
}

/*
 * TODO:
 * Дописать этот класс
 * Рефакторить тесты
 * 
 */