namespace VideocartLab.MainModelsProj.Screen
{
    /// <summary>
    /// Стандартные настройки для разных версий порта DisplayPort
    /// </summary>
    public static class DisplayPort
    {
        public static ScreenInterface DP2_1080p => ScreenInterfaceConnections.CreateInterface(77.37d, 240, 8, Solutions._1080p);
        public static ScreenInterface DP2_720p => ScreenInterfaceConnections.CreateInterface(77.37d, 240, 8, Solutions._720p);
        public static ScreenInterface DP2_4k => ScreenInterfaceConnections.CreateInterface(77.37d, 240, 8, Solutions._4k);
        public static ScreenInterface DP2_8k => ScreenInterfaceConnections.CreateInterface(77.37d, 85, 8, Solutions._8k);

        public static ScreenInterface DP1dot4_1080p => ScreenInterfaceConnections.CreateInterface(25.92d, 360, 8, Solutions._1080p);
        public static ScreenInterface DP1dot4_720p => ScreenInterfaceConnections.CreateInterface(25.92d, 360, 8, Solutions._720p);
        public static ScreenInterface DP1dot4_4k => ScreenInterfaceConnections.CreateInterface(25.92d, 120, 8, Solutions._4k);
        public static ScreenInterface DP1dot4_8k => ScreenInterfaceConnections.CreateInterface(25.92d, 30, 8, Solutions._8k);

        public static ScreenInterface DP1dot2_1080p => ScreenInterfaceConnections.CreateInterface(17.28d, 240, 8, Solutions._1080p);
        public static ScreenInterface DP1dot2_720p => ScreenInterfaceConnections.CreateInterface(17.28d, 240, 8, Solutions._720p);
        public static ScreenInterface DP1dot2_4k => ScreenInterfaceConnections.CreateInterface(17.28d, 75, 8, Solutions._4k);

        public static ScreenInterface DP1_1080p => ScreenInterfaceConnections.CreateInterface(8.64d, 144, 8, Solutions._1080p);
        public static ScreenInterface DP1_720p => ScreenInterfaceConnections.CreateInterface(8.64d, 144, 8, Solutions._720p);
        public static ScreenInterface DP1_4k => ScreenInterfaceConnections.CreateInterface(8.64d, 30, 8, Solutions._4k);
    }
}
