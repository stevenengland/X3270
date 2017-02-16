namespace StEn.X3270.Rest
{
    using System.Threading;
    using System.Threading.Tasks;

    using StatusText;

    /// <summary>
    /// Interface that holds all the possible actions.
    /// </summary>
    internal interface IActions
    {
        // todo RoadMap
        // *Attn - The 3270 ATTN key is interpreted by many host applications in an SNA environment as an indication that the user wishes to interrupt the execution of the current process.
        // AnsiText - Outputs whatever data that has been output by the host in NVT mode since the last time that AnsiText was called.
        // Ascii(row,col,rows,cols) 
        // Ascii(row, col, length)
        // Ascii(length)
        // BackSpace
        // BackTab
        // CircumNot
        // *Clear
        // Compose
        // * CursorSelect
        // Cut
        // Delete
        // DeleteField
        // DeleteWord
        // * Disconnect
        // Down
        // Dup
        // Ebcdic(row, col, rows, cols)
        // Ebcdic(row, col, length)
        // Ebcdic(length)
        // Ebcdic
        // EbcdicField
        // Erase
        // EraseEOF
        // EraseInput
        // Execute("command")
        // FieldEnd
        // FieldMark
        // HexString
        // Home
        // Info - In x3270, pops up an informational message. In c3270 and wc3270, writes an informational message to the OIA (the line below the display). Not defined for s3270 or tcl3270.
        // Insert
        // ignore
        // Keymap - Adds or removes a temporary keymap. If the keymap parameter is given, the named keymap is added. If no parameter is given, the most recently added keymap is removed. -No such keymap resource or file: Apl
        // Left
        // Left2
        // Macro("name")
        // MonoCase
        // MoveCursor[(row,col)]
        // Newline
        //
        //NextWord
        //PreviousWord
        // PrintText([[html|rtf,file,filename]|string|modi|caption,text][append,][replace,]file,filename) PrintText%28html,file,test.txt%29
        // PrintText%28rtf,test.txt%29
        // PrintText([command,]filter) <- Parameter drehen, Pipes an ASCII representation of the current screen image through the named filter, e.g., lpr.
        // PrintText(html, string) <- Returns the current screen contents as HTML.
        // Query(keyword) keyword as Enum
        // Quit
        // ReadBuffer(Ascii)
        // ReadBuffer(Ebcdic)
        // RedrawWindow
        // Reset
        // Right
        // Right2
        // Scroll(Forward|Backward)
        // Script(path[,arg...])
        // Snap
        // Snap(Ascii) Snap%28Ascii,0,0,10
        // Snap(Cols)
        // Snap(Ebcdic,...)
        // Snap(ReadBuffer)
        // Snap(Rows)
        // Snap(Save)
        // Snap(Status)
        // Source(File)
        // Title(Text)
        // Up

        /// <summary>
        /// Receives an ASCII text representation of the screen contents.
        /// <remarks>Start x3270 with UTF8 switch if you need characters that are not included in ASCII.</remarks>
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ascii(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Receives an ASCII text representation of the field containing the cursor.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> AsciiField(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Send Enter command.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Enter(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Insert a single character at the cursor position.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Key(char key, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Program Function AID (n from 1 to 24). May Block.
        /// </summary>
        /// <param name="programFunctionKey">
        /// The PF [1..24] key.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Pf(int programFunctionKey, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Executes a raw string as command.
        /// </summary>
        /// <param name="rawCommand">
        /// The raw Command. For example "PF(3)".
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Raw(string rawCommand, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Send Tab command.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Tab(CancellationToken cancellationToken = default(CancellationToken));
    }
}
