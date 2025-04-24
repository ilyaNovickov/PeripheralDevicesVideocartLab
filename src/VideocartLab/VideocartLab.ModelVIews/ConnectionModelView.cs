using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class ConnectionModelView : ModelViewBase
    {
        private Connector connector;

        private double xOffset;
        private double yOffset;

        public ConnectionModelView() 
        {
            Model = new Connector();
            Model.ParentChanged += Model_ParentChanged;
        }

        private void Model_ParentChanged(object? sender, ConnectorParentArgs e)
        {
            if (e.Parent == null)
                return;

            xOffset = X - e.Parent.X;
            yOffset = Y - e.Parent.Y;

            e.Parent.Moved += Parent_Moved;
        }

        private void Parent_Moved(object? sender, NodeMovedArgs e)
        {
            this.X = e.X + xOffset;
            this.Y = e.Y + yOffset;
        }

        internal Connector Model
        {
            get => connector;
            private set => connector = value;
        }

        public double X
        {
            get => connector.X;
            set
            {
                connector.X = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => connector.Y;
            set
            {
                connector.Y = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => connector.Width;
            set
            {
                connector.Width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => connector.Height;
            set
            {
                connector.Height = value;
                OnPropertyChanged();
            }
        }
    }
}
