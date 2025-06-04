namespace VideocartLab.ModelViews.Models
{
    /// <summary>
    /// Аргументы события формирования отчёта
    /// </summary>
    internal class ReportArgs : EventArgs
    {
        /// <summary>
        /// Сообщение отчёта
        /// </summary>
        public string Message { get; private set; }

        public ReportArgs(string message)
        {
            Message = message;
        }
    }
}
