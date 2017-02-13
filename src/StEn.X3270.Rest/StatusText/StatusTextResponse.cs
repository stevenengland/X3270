namespace StEn.X3270.Rest.StatusText
{
    using System;
    using System.Text;

    public class StatusTextResponse<T> : IResponse<T>
    {
        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Gets the command execution time. The time that it took for the host to respond to the previous command, in seconds with milliseconds after the decimal. If the previous command did not require a host response, this is a dash. 
        /// </summary>
        public TimeSpan CommandExecutionTime { get; internal set; }

        /// <summary>
        /// Gets the encoding of the result.
        /// </summary>
        public Encoding Encoding { get; internal set; }

        /// <summary>
        /// Gets the keyboard state. If the keyboard is unlocked, the letter U. If the keyboard is locked waiting for a response from the host, or if not connected to a host, the letter L. If the keyboard is locked because of an operator error (field overflow, protected field, etc.), the letter E. 
        /// </summary>
        public string KeyboardState { get; internal set; }

        /// <summary>
        /// Gets the screen formatting. If the screen is formatted, the letter F. If unformatted or in NVT mode, the letter U. 
        /// </summary>
        public string ScreenFormatting { get; internal set; }

        /// <summary>
        /// Gets the field protection. If the field containing the cursor is protected, the letter P. If unprotected or unformatted, the letter U. 
        /// </summary>
        public string FieldProtection { get; internal set; }

        /// <summary>
        /// Gets the connection state. If connected to a host, the string C(hostname). Otherwise, the letter N. 
        /// </summary>
        public string ConnectionState { get; internal set; }

        /// <summary>
        /// Gets the emulator mode. If connected in 3270 mode, the letter I. If connected in NVT line mode, the letter L. If connected in NVT character mode, the letter C. If connected in not negotiated mode (no BIND active from the host), the letter P. If not connected, the letter N. 
        /// </summary>
        public string EmulatorMode { get; internal set; }

        /// <summary>
        /// Gets the emulated model number.
        /// Model number    Columns     Rows
        /// 2               80          24
        /// 3               80          32
        /// 4               80          43
        /// 5               132         27
        /// </summary>
        public int ModelNumber { get; internal set; }

        /// <summary>
        /// Gets the number of rows. The current number of rows defined on the screen. The host can request that the emulator use a 24x80 screen, so this number may be smaller than the maximum number of rows possible with the current model. 
        /// </summary>
        public int NumberOfRows { get; internal set; }

        /// <summary>
        /// Gets the number of columns. The current number of columns defined on the screen, subject to the same difference for rows, above. 
        /// </summary>
        public int NumberOfColumns { get; internal set; }

        /// <summary>
        /// Gets the cursor row. The current cursor row (zero-origin). 
        /// </summary>
        public int CursorRow { get; internal set; }

        /// <summary>
        /// Gets the cursor column. The current cursor column (zero-origin). 
        /// </summary>
        public int CursorColumn { get; internal set; }

        /// <summary>
        /// Gets the window identifier. The X window identifier for the main x3270 window, in hexadecimal preceded by 0x. For <c>ws3270</c> and <c>wc3270</c>, this is zero.  
        /// </summary>
        public string WindowId { get; internal set; }

        /// <summary>
        /// Gets the output from the operation.
        /// </summary>
        public T PayLoad { get; internal set; }
    }
}
