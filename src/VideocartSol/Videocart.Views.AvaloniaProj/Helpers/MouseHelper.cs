using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.ViewModel.Extra;

namespace Videocart.Views.AvaloniaProj.Helpers
{
    public static class MouseHelper
    {
        public static MouseButton GetButton(Avalonia.Input.PointerPointProperties properties)
        {
            if (properties.IsLeftButtonPressed)
                return MouseButton.Left;
            else if (properties.IsRightButtonPressed)
                return MouseButton.Right;
            else if (properties.IsMiddleButtonPressed)
                return MouseButton.Middle;
            else
                return MouseButton.Undef;
        }
    }
}
