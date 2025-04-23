using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Views.Avalonia.Helpers
{
    public static class MouseButtonHelper
    {
        public static VideocartLab.ModelVIews.MouseButton GetMouseButton(PointerPoint pointer)
        {
            if (pointer.Properties.IsLeftButtonPressed)
                return VideocartLab.ModelVIews.MouseButton.Left;
            else if (pointer.Properties.IsRightButtonPressed)
                return VideocartLab.ModelVIews.MouseButton.Right;
            else if (pointer.Properties.IsMiddleButtonPressed)
                return VideocartLab.ModelVIews.MouseButton.Middle;
            else
                return VideocartLab.ModelVIews.MouseButton.Undef;
        }
    }
}
