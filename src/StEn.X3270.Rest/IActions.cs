// ReSharper disable UnusedMemberInSuper.Global
namespace StEn.X3270.Rest
{
    using System.Threading;
    using System.Threading.Tasks;

    using StatusText;

    using StEn.X3270.Rest.Types.Enums;

    /// <summary>
    /// Interface that holds all the possible actions.
    /// </summary>
    internal interface IActions
    {
        /// <summary>
        /// Outputs whatever data that has been output by the host in NVT mode since the last time that <c>AnsiText</c> was called.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> AnsiText(CancellationToken cancellationToken = default(CancellationToken));

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
        /// Receives an ASCII text representation of the screen contents.
        /// A rectangular region of the screen is output. (Note that the row and column are zero-origin.) 
        /// <remarks>
        /// Start x3270 with UTF8 switch if you need characters that are not included in ASCII.
        /// </remarks>
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="rows">
        /// The row span.
        /// </param>
        /// <param name="cols">
        /// The col span.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ascii(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Receives an ASCII text representation of the screen contents.
        /// <remarks>
        /// Start x3270 with UTF8 switch if you need characters that are not included in ASCII.
        /// </remarks>
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="length">
        /// The length of characters that are output, starting at row/col.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ascii(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Receives an ASCII text representation of the screen contents.
        /// <remarks>
        /// Start x3270 with UTF8 switch if you need characters that are not included in ASCII.
        /// </remarks>
        /// </summary>
        /// <param name="length">
        /// The length of characters that are output, starting at the cursor position.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ascii(
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

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
        /// The 3270 ATTN key is interpreted by many host applications in an SNA environment as an indication that the user wishes to interrupt the execution of the current process.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Attn(CancellationToken cancellationToken = default(CancellationToken));

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
        /// The same function as <c>Ascii</c>, except that rather than generating ASCII text, each character is output as a 2-digit or 4-digit hexadecimal EBCDIC code. 
        /// A rectangular region of the screen is output. (Note that the row and column are zero-origin.) 
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="rows">
        /// The row span.
        /// </param>
        /// <param name="cols">
        /// The col span.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ebcdic(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The same function as <c>Ascii</c>, except that rather than generating ASCII text, each character is output as a 2-digit or 4-digit hexadecimal EBCDIC code. 
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="length">
        /// The length of characters that are output, starting at row/col.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ebcdic(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// The same function as <c>Ascii</c>, except that rather than generating ASCII text, each character is output as a 2-digit or 4-digit hexadecimal EBCDIC code. 
        /// </summary>
        /// <param name="length">
        /// The length of characters that are output, starting at the cursor position.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Ebcdic(
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

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
        /// Execute a command in a shell.
        /// <remarks>
        /// Put the command in double quotes if it contains whitespaces etc. Like "echo hello".
        /// </remarks>
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Execute(
            string command,
            CancellationToken cancellationToken = default(CancellationToken));

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
        Task<StatusTextResponse<string>> HexString(
            string hex,
            CancellationToken cancellationToken = default(CancellationToken));

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
        Task<StatusTextResponse<string>> Info(
            string message,
            CancellationToken cancellationToken = default(CancellationToken));

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
        /// Adds or removes a temporary key map. If the key map parameter is given, the named key map is added. If no parameter is given, the most recently added key map is removed.
        /// </summary>
        /// <param name="keymap">
        /// The key map.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Keymap(
            string keymap = "",
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor left.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Left(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor left 2 positions.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Left2(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Run a macro.
        /// </summary>
        /// <param name="macroName">
        /// The macro name.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Macro(
            string macroName,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Toggle uppercase-only mode.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> MonoCase(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor to coordinate from zero-origin (row,col).
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> MoveCursor(
            int row,
            int col,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor to first field on next line.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Newline(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor to next word.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> NextWord(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor to previous word.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> PreviousWord(CancellationToken cancellationToken = default(CancellationToken));

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
        Task<StatusTextResponse<string>> Pf(
            int programFunctionKey,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Print the current screen content via printer dialog.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        // ReSharper disable once UnusedMember.Global because it creates a blocking printer dialogmove
        Task<StatusTextResponse<string>> PrintText(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Print the current screen content.
        /// </summary>
        /// <param name="printFormat">
        /// The print format to use.
        /// </param>
        /// <param name="printToScreen">
        /// Determines whether the output shall be sent to printer or to screen.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> PrintText(
            PrintFormat printFormat,
            bool printToScreen,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Print the current screen content.
        /// </summary>
        /// <param name="printFormat">
        /// The print format to use.
        /// </param>
        /// <param name="file">
        /// The file where the contents shall be saved.
        /// </param>
        /// <param name="append">
        /// <c>true</c> means the content will be appended, <c>false</c> means the file will be replaced.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> PrintText(
            PrintFormat printFormat,
            string file,
            bool append = false,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns state information. Without a keyword, Query returns each of the defined attributes, one per line, labeled by its name. 
        /// </summary>
        /// <param name="keyword">
        /// The keyword.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Query(
            QueryKeyword keyword = QueryKeyword.None,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Exit <c>x3270</c>.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Quit(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Dumps the contents of the screen buffer, one line at a time. Positions inside data fields are generally output as 2-digit hexadecimal codes in the current display character set. If the current locale specifies UTF-8 (or certain DBCS character sets), some positions may be output as multi-byte strings (4-, 6- or 8-digit codes). DBCS characters take two positions in the screen buffer; the first location is output as a multi-byte string in the current locale code set, and the second location is output as a dash. Start-of-field characters (each of which takes up a display position) are output as <c>SF(aa=nn[,...])</c>, where <c>aa</c> is a field attribute type and <c>nn</c> is its value.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> ReadBufferAsAscii(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Equivalent to <c>ReadBuffer(Ascii)</c>, but with the data fields output as hexadecimal EBCDIC codes instead. Additionally, if a buffer position has the Graphic Escape attribute, it is displayed as GE(xx).
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> ReadBufferAsEbcdic(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Redraws the window.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Redraw(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Resets locked keyboards.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Reset(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor right.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Right(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Move cursor right 2 positions.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Right2(CancellationToken cancellationToken = default(CancellationToken));

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
        Task<StatusTextResponse<string>> Raw(
            string rawCommand,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Runs a child script, passing it optional command-line arguments. path must specify an executable (binary) program: the emulator will create a new process and execute it. If you simply want the emulator to read commands from a file, use the <c>Source</c> action.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Script(
            string file,
            string[] args = null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scroll screen forward.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> ScrollForward(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Scroll screen backward.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> ScrollBackward(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Saves a copy of the screen image and status in a temporary buffer. This copy can be queried with other Snap actions to allow a script to examine a consistent screen image, even when the host may be changing the image (or even the screen dimensions) dynamically. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Snap(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ascii</c> action on the saved screen image.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapAscii(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ascii</c> action on the saved screen image.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="rows">
        /// The row span.
        /// </param>
        /// <param name="cols">
        /// The col span.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapAscii(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ascii</c> action on the saved screen image.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="length">
        /// The length of characters that are output, starting at row/col.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapAscii(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ascii</c> action on the saved screen image.
        /// </summary>
        /// <param name="length">
        /// The length of characters that are output, starting at the cursor position.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapAscii(
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the number of columns in the saved screen image. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapCols(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ebcdic</c> action on the saved screen image.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapEbcdic(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ebcdic</c> action on the saved screen image.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="rows">
        /// The row span.
        /// </param>
        /// <param name="cols">
        /// The col span.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapEbcdic(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ebcdic</c> action on the saved screen image.
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="col">
        /// The col.
        /// </param>
        /// <param name="length">
        /// The length of characters that are output, starting at row/col.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapEbcdic(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the <c>Ebcdic</c> action on the saved screen image.
        /// </summary>
        /// <param name="length">
        /// The length of characters that are output, starting at the cursor position.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapEbcdic(
            int length,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the number of rows in the saved screen image.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapRows(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Performs the ReadBuffer action on the saved screen image. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapReadBuffer(
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Returns the status line from when the screen was last saved.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> SnapStatus(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Read and execute commands from file. Any output from those commands will become the output from Source. If any of the commands fails, the Source command will not abort; it will continue reading commands until EOF.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Source(
            string file,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Changes the <c>wc3270</c> window title to text. 
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<StatusTextResponse<string>> Title(
            string text,
            CancellationToken cancellationToken = default(CancellationToken));

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