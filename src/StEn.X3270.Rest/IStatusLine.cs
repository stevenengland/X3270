namespace StEn.X3270.Rest
{
    using System;

    // Taken from http://x3270.bgp.nu/x3270-script.html#Status-Format

    /// <summary>
    /// The status message consists of 12 blank-separated fields:
    /// </summary>
    public interface IStatusLine
    {
        // todo: create Enums

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <value>
        /// The state of the keyboard.
        /// </value>
        string KeyboardState { get; }

        /// <summary>
        /// Gets the screen formatting.
        /// </summary>
        /// <value>
        /// The screen formatting.
        /// </value>
        string ScreenFormatting { get; }

        /// <summary>
        /// Gets the field protection.
        /// </summary>
        /// <value>
        /// The field protection.
        /// </value>
        string FieldProtection { get; }

        /// <summary>
        /// Gets the state of the connection.
        /// </summary>
        /// <value>
        /// The state of the connection.
        /// </value>
        string ConnectionState { get; }

        /// <summary>
        /// Gets the emulator mode.
        /// </summary>
        /// <value>
        /// The emulator mode.
        /// </value>
        string EmulatorMode { get; }

        /// <summary>
        /// Gets the model number.
        /// </summary>
        /// <value>
        /// The model number.
        /// </value>
        int ModelNumber { get; }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        /// <value>
        /// The number of rows.
        /// </value>
        int NumberOfRows { get; }

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        /// <value>
        /// The number of columns.
        /// </value>
        int NumberOfColumns { get; }

        /// <summary>
        /// Gets the cursor row.
        /// </summary>
        /// <value>
        /// The cursor row.
        /// </value>
        int CursorRow { get; }

        /// <summary>
        /// Gets the cursor column.
        /// </summary>
        /// <value>
        /// The cursor column.
        /// </value>
        int CursorColumn { get; }

        /// <summary>
        /// Gets the window identifier.
        /// </summary>
        /// <value>
        /// The window identifier.
        /// </value>
        string WindowId { get; }

        /// <summary>
        /// Gets the command execution time.
        /// </summary>
        /// <value>
        /// The command execution time.
        /// </value>
        TimeSpan CommandExecutionTime { get; }
    }
}