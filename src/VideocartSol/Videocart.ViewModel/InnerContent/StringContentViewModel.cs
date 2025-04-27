using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.ViewModel.InnerContent
{
    public class StringContentViewModel : ViewModelBase
    {
        private string str = "";

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
