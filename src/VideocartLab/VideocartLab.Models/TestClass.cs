using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.ExtraAbstractions;

namespace VideocartLab.Models
{
    public class TestClass : NotifyPropertyObject
    {
        private int count = 0;
        private string str = "Test";

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        public string Str
        {
            get => str;
            set
            {
                str = value;
                OnPropertyChanged();
            }
        }
    }
}
