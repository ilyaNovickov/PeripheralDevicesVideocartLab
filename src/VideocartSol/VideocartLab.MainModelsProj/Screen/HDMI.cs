namespace VideocartLab.MainModelsProj.Screen
{
    /// <summary>
    /// Стандратные настройки для разных версий порта HDMI
    /// </summary>
    public static class HDMI
    {
        public static ScreenInterface HDMI1dot3_1080p => ScreenInterfaceConnections.CreateInterface(8.16d, 144, 8, Solutions._1080p);
        public static ScreenInterface HDMI1dot3_720p => ScreenInterfaceConnections.CreateInterface(8.16d, 144, 8, Solutions._720p);
        public static ScreenInterface HDMI1dot3_4k => ScreenInterfaceConnections.CreateInterface(8.16d, 30, 8, Solutions._4k);

        public static ScreenInterface HDMI2_1080p => ScreenInterfaceConnections.CreateInterface(14.4d, 240, 8, Solutions._1080p);
        public static ScreenInterface HDMI2_720p => ScreenInterfaceConnections.CreateInterface(14.4d, 240, 8, Solutions._720p);
        public static ScreenInterface HDMI2_4k => ScreenInterfaceConnections.CreateInterface(14.4d, 60, 8, Solutions._4k);

        public static ScreenInterface HDMI2dot1_1080p => ScreenInterfaceConnections.CreateInterface(42.6d, 240, 8, Solutions._1080p);
        public static ScreenInterface HDMI2dot1_720p => ScreenInterfaceConnections.CreateInterface(42.6d, 240, 8, Solutions._720p);
        public static ScreenInterface HDMI2dot1_4k => ScreenInterfaceConnections.CreateInterface(42.6d, 144, 8, Solutions._4k);
        public static ScreenInterface HDMI2dot1_8k => ScreenInterfaceConnections.CreateInterface(42.6d, 30, 8, Solutions._8k);
    }
}
