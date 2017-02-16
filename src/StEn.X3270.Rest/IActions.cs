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
        
        // Ebcdic(row, col, rows, cols)
        // Ebcdic(row, col, length)
        // Ebcdic(length)

        // Execute("command")
        // Keymap - Adds or removes a temporary keymap. If the keymap parameter is given, the named keymap is added. If no parameter is given, the most recently added keymap is removed. -No such keymap resource or file: Apl
        // Left
        // Left2
        // Macro("name")
        // MonoCase
        // MoveCursor[(row,col)]
        // Newline
        // NextWord
        // PreviousWord
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
        /// Move cursor left.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> BackSpace(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Tab to start of previous input field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> BackTab(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Input "^" in NVT mode, or "¬" in 3270 mode.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> CircumNot(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Clear(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The next two keys form a special symbol.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Compose(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Cursor Select AID.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> CursorSelect(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Copy highlighted area to clipboard and erase.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Cut(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete character under cursor (or send ASCII DEL).
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Delete(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete the entire field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> DeleteField(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete the current or previous word.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> DeleteWord(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Disconnect from host.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Disconnect(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor down.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Down(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Duplicate field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Dup(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The same function as <c>Ascii</c>, except that rather than generating ASCII text, each character is output as a 2-digit or 4-digit hexadecimal EBCDIC code. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ebcdic(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The same function as <c>AsciiField</c> above, except that it generates hexadecimal EBCDIC codes.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> EbcdicField(CancellationToken cancellationToken = default(CancellationToken));

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
        /// Erase previous character.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Erase(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Erase to end of current field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> EraseEof(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Erase all input fields.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> EraseInput(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor to end of field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> FieldEnd(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Mark field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> FieldMark(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Insert control-character string.
        /// </summary>
        /// <param name="hex">
        /// The hexadecimal string.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> HexString(string hex, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor to first input field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Home(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// In <c>x3270</c>, pops up an informational message. In <c>c3270</c> and <c>wc3270</c>, writes an informational message to the OIA (the line below the display). Not defined for <c>s3270</c> or<c>tcl3270</c>. 
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Info(string message, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Set insert mode.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Insert(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Do nothing.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ignore(CancellationToken cancellationToken = default(CancellationToken));

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

        /// <summary>
        /// Move cursor up.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Up(CancellationToken cancellationToken = default(CancellationToken));
    }
}
