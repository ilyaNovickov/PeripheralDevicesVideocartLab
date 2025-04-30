using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.Screen
{
    public static class DisplayPort
    {
        public static ScreenInterface DP2_1080p => ScreenInterfaceConnections.CreateInterface(77.37d, 240, 24, Solutions._1080p);
        public static ScreenInterface DP2_720p => ScreenInterfaceConnections.CreateInterface(77.37d, 240, 24, Solutions._720p);
        public static ScreenInterface DP2_4k => ScreenInterfaceConnections.CreateInterface(77.37d, 240, 24, Solutions._4k);
        public static ScreenInterface DP2_8k => ScreenInterfaceConnections.CreateInterface(77.37d, 85, 24, Solutions._8k);

        public static ScreenInterface DP1dot4_1080p => ScreenInterfaceConnections.CreateInterface(25.92d, 360, 24, Solutions._1080p);
        public static ScreenInterface DP1dot4_720p => ScreenInterfaceConnections.CreateInterface(25.92d, 360, 24, Solutions._720p);
        public static ScreenInterface DP1dot4_4k => ScreenInterfaceConnections.CreateInterface(25.92d, 120, 24, Solutions._4k);
        public static ScreenInterface DP1dot4_8k => ScreenInterfaceConnections.CreateInterface(25.92d, 30, 24, Solutions._8k);

        public static ScreenInterface DP1dot2_1080p => ScreenInterfaceConnections.CreateInterface(17.28d, 240, 24, Solutions._1080p);
        public static ScreenInterface DP1dot2_720p => ScreenInterfaceConnections.CreateInterface(17.28d, 240, 24, Solutions._720p);
        public static ScreenInterface DP1dot2_4k => ScreenInterfaceConnections.CreateInterface(17.28d, 75, 24, Solutions._4k);

        public static ScreenInterface DP1_1080p => ScreenInterfaceConnections.CreateInterface(8.64d, 144, 24, Solutions._1080p);
        public static ScreenInterface DP1_720p => ScreenInterfaceConnections.CreateInterface(8.64d, 144, 24, Solutions._720p);
        public static ScreenInterface DP1_4k => ScreenInterfaceConnections.CreateInterface(8.64d, 30, 24, Solutions._4k);
    }
}
