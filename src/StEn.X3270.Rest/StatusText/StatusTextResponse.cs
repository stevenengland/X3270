namespace StEn.X3270.Rest.StatusText
{
    using System;
    using System.Text;

    public class StatusTextResponse<T> : IResponse<T>
    {
        public bool Success { get; internal set; }

        public TimeSpan CommandExecutionTime { get; internal set; }

        public Encoding Encoding { get; internal set; }

        public string KeyboardState { get; internal set; }

        public string ScreenFormatting { get; internal set; }

        public string FieldProtection { get; internal set; }

        public string ConnectionState { get; internal set; }

        public string EmulatorMode { get; internal set; }

        public int ModelNumber { get; internal set; }

        public int NumberOfRows { get; internal set; }

        public int NumberOfColumns { get; internal set; }

        public int CursorRow { get; internal set; }

        public int CursorColumn { get; internal set; }

        public string WindowId { get; internal set; }

        public T PayLoad { get; internal set; }
    }
}
