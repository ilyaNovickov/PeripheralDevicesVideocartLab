using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public class NodeModelView : ModelViewBase
    {
        private double x;
        private double y;
        private double width;
        private double height;
        private ModelViewBase? innerContent = null;
        private string? name = null;

        #region Properties
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }
        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }
        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        public ModelViewBase? InnerContent
        {
            get => innerContent;
            set
            {
                innerContent = value;
                OnPropertyChanged();
            }
        }
        public string? Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        #endregion
    }
}
