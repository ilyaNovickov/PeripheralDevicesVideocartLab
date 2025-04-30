using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.Screen
{
    internal enum Solutions
    {
        _1080p, _4k, _8k, _720p
    }

    internal static class ScreenInterfaceConnections
    {
        internal static ScreenInterface CreateInterface(double bandwidth, double frequency, int bitPerPix, Solutions solution)
        {
            switch (solution)
            {
                case Solutions._1080p:
                    return new ScreenInterface(bandwidth, frequency, bitPerPix, 1920, 1080);
                case Solutions._4k:
                    return new ScreenInterface(bandwidth, frequency, bitPerPix, 3840, 2160);
                case Solutions._8k:
                    return new ScreenInterface(bandwidth, frequency, bitPerPix, 7680, 4320);
                case Solutions._720p:
                    return new ScreenInterface(bandwidth, frequency, bitPerPix, 1280, 720);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
