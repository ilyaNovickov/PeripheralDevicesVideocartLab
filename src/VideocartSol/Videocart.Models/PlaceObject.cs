using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Models
{
    public class PlaceObject : NotifyObject
    {
        private double x;
        private double y;
        private double width;
        private double height;

        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Center));
            }
        }

        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Center));
            }
        }

        public Point Center
        {
            get => new Point(X + Width / 2d, Y + Height / 2d);
        }

        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Center));
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Center));
            }
        }
    }
}
