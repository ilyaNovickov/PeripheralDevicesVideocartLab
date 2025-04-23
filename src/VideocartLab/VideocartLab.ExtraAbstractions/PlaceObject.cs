namespace VideocartLab.ExtraAbstractions
{
    public class PlaceObject : NotifyPropertyObject, IPlaceObject
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
    }
}
