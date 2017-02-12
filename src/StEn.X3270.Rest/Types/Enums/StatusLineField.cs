namespace StEn.X3270.Rest.Types.Enums
{
    /// <summary>
    /// Definitions of the fields on the status line.
    /// </summary>
    public enum StatusLineField
    {
        /// <summary>
        /// Keyboard: unlocked (U), locked (L), error lock (E)
        /// </summary>
        KeyboardState,

        /// <summary>
        /// ScreenFormatting: formatted (F), unformatted (U)
        /// </summary>
        ScreenFormatting,

        /// <summary>
        /// Protection status of current field: unprotected (U), protected (P)
        /// </summary>
        FieldProtection,

        /// <summary>
        /// Connection status: not connected (N), connected (C), plus hostname
        /// </summary>
        ConnectionState,

        /// <summary>
        /// Mode: not connected (N), connected in NVT character mode (C),
        ///  connected in NVT line mode (L), 3270 negotiation pending (P),
        ///  connected in 3270 mode (I).
        /// </summary>
        EmulatorMode,

        /// <summary>
        /// Model number (2/3/4/5)
        /// </summary>
        ModelNumber,

        /// <summary>
        /// Number of rows on current screen.
        /// </summary>
        NumberOfRows,

        /// <summary>
        /// Number of columns on current screen.
        /// </summary>
        NumberOfColumns,

        /// <summary>
        /// Row containing cursor.
        /// </summary>
        CursorRow,

        /// <summary>
        /// Column containing cursor.
        /// </summary>
        CursorColumn,

        /// <summary>
        /// X11 window ID of main window, of 0 if not applicable
        /// </summary>
        WindowId,

        /// <summary>
        /// Time that last command took to execute, or -
        /// </summary>
        CommandExecutionTime
    }
}