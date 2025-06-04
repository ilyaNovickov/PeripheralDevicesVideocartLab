namespace VideocartLab.MainModelsProj
{
    /// <summary>
    /// Класс для определения правильности введённых значений
    /// </summary>
    public static class ValuesValidator
    {
        public static void ValidUnnegativeArgument(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "This value can't be negative");
        }
        public static void ValidUnnegativeArgument(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "This value can't be negative");
        }

        public static void ValidUnnullObject(object? value)
        {
            if (null == value)
                throw new ArgumentNullException(nameof(value), "This value can't be null");
        }
    }
}
