namespace StEn.X3270.Rest.StatusText
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;

    using Converter;

    using Types;
    using Types.Enums;
    using Types.Exception;

    /// <summary>
    /// An API client for the SText REST endpoint of x3270.
    /// </summary>
    public sealed class StatusTextApiClient : IApiClient, IDisposable
    {
        /// <summary>
        /// The rest root.
        /// </summary>
        private const string RestRoot = "/3270/rest/stext/";

        /// <summary>
        /// The HTTP client used for requesting the REST endpoint of x3270.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Indicates if this instance is disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusTextApiClient"/> class.
        /// </summary>
        /// <param name="hostAddress">
        /// The host address.
        /// </param>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <param name="webProxy">
        /// The web proxy.
        /// </param>
        public StatusTextApiClient(string hostAddress, int port, IWebProxy webProxy = null)
        {
            this.Port = port;
            this.HostAddress = hostAddress;

            this.WebProxy = webProxy;

            var httpClientHandler = new HttpClientHandler();

            if (this.WebProxy != null)
            {
                httpClientHandler.UseProxy = true;
                httpClientHandler.Proxy = this.WebProxy;
            }

            this.httpClient = new HttpClient(httpClientHandler);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="StatusTextApiClient"/> class. 
        /// </summary>
        ~StatusTextApiClient()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public int Port { get; }

        /// <summary>
        /// Gets the host address.
        /// </summary>
        /// <value>
        /// The host address.
        /// </value>
        public string HostAddress { get; }

        /// <summary>
        /// Timeout for uploading Files/Videos/Documents etc.
        /// </summary>
        public TimeSpan UploadTimeout { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// Gets the web proxy.
        /// </summary>
        public IWebProxy WebProxy { get; }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Check connection status.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <exception cref="RequestException">The Server responded but with unexpected content</exception>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public async Task<HtmlResponse> CheckConnectionStatus(
            CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            var uri = new Uri(this.HostAddress + ":" + this.Port + "/3270/rest/");

            try
            {
                var response = await this.httpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);

                var responseStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                HtmlResponse htmlResponse;
                if (HtmlResponse.TryParseRestRoot(responseStr, out htmlResponse)) return htmlResponse;
                throw new RequestException("The Server responded but with unexpected content");
            }
            catch (Exception ex)
            {
                return new HtmlResponse(ex.Message);
            }
        }

        /// <summary>
        /// Outputs whatever data that has been output by the host in NVT mode since the last time that <c>AnsiText</c> was called.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> AnsiText(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("AnsiText", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ascii(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Ascii", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ascii(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Ascii({row},{col},{rows},{cols})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ascii(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Ascii({row},{col},{length})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ascii(
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Ascii({length})", null, cancellationToken);
        }

        /// <summary>
        /// Receives an ASCII text representation of the field containing the cursor.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> AsciiField(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("AsciiField", null, cancellationToken);
        }

        /// <summary>
        /// The 3270 ATTN key is interpreted by many host applications in an SNA environment as an indication that the user wishes to interrupt the execution of the current process.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Attn(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Attn", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor left.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> BackSpace(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("BackSpace", null, cancellationToken);
        }

        /// <summary>
        /// Tab to start of previous input field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> BackTab(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("BackTab", null, cancellationToken);
        }

        /// <summary>
        /// Input "^" in NVT mode, or "¬" in 3270 mode.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> CircumNot(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("CircumNot", null, cancellationToken);
        }

        /// <summary>
        /// Clears the screen.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Clear(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Clear", null, cancellationToken);
        }

        /// <summary>
        /// The next two keys form a special symbol. For example, "Compose" followed by the "C" key and the "," (comma) key, enters the "C-cedilla" symbol. A C on the status line indicates a pending composite character.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Compose(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Compose", null, cancellationToken);
        }

        /// <summary>
        /// Cursor Select AID.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> CursorSelect(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("CursorSelect", null, cancellationToken);
        }

        /// <summary>
        /// Copy highlighted area to clipboard and erase.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Cut(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Cut", null, cancellationToken);
        }

        /// <summary>
        /// Delete character under cursor (or send ASCII DEL)
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Delete(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Delete", null, cancellationToken);
        }

        /// <summary>
        /// Delete the entire field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> DeleteField(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("DeleteField", null, cancellationToken);
        }

        /// <summary>
        /// Delete the current or previous word.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> DeleteWord(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("DeleteWord", null, cancellationToken);
        }

        /// <summary>
        /// Disconnect from host.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Disconnect(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Disconnect", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor down.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Down(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Down", null, cancellationToken);
        }

        /// <summary>
        /// Duplicate field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Dup(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Dup", null, cancellationToken);
        }

        /// <summary>
        /// The same function as <c>Ascii</c>, except that rather than generating ASCII text, each character is output as a 2-digit or 4-digit hexadecimal EBCDIC code. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Ebcdic(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Ebcdic", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ebcdic(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Ebcdic({row},{col},{rows},{cols})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ebcdic(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Ebcdic({row},{col},{length})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Ebcdic(
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Ebcdic({length})", null, cancellationToken);
        }

        /// <summary>
        /// The same function as <c>AsciiField</c> above, except that it generates hexadecimal EBCDIC codes.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> EbcdicField(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("EbcdicField", null, cancellationToken);
        }

        /// <summary>
        /// Erase previous character.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Erase(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Erase", null, cancellationToken);
        }

        /// <summary>
        /// Erase to end of current field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> EraseEof(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("EraseEOF", null, cancellationToken);
        }

        /// <summary>
        /// Erase all input fields.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> EraseInput(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("EraseInput", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Execute(
            string command,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Execute({command})", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor to end of field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> FieldEnd(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("FieldEnd", null, cancellationToken);
        }

        /// <summary>
        /// Mark field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> FieldMark(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("FieldMark", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> HexString(
            string hex,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"HexString({hex})", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor to first input field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Home(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Home", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Info(
            string message,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Info({message})", null, cancellationToken);
        }

        /// <summary>
        /// Set insert mode.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Insert(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Insert", null, cancellationToken);
        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Ignore(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("ignore", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Keymap(
            string keymap = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Keymap({keymap})", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor left.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Left(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Left", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor left 2 positions.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Left2(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Left2", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Macro(
            string macroName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Macro({macroName})", null, cancellationToken);
        }

        /// <summary>
        /// Toggle uppercase-only mode.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> MonoCase(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("MonoCase", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> MoveCursor(
            int row,
            int col,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"MoveCursor({row},{col})", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor to first field on next line.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Newline(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("NewLine", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor to next word.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> NextWord(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("NextWord", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor to previous word.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> PreviousWord(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("PreviousWord", null, cancellationToken);
        }

        /// <summary>
        /// Print the current screen content via printer dialog.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> PrintText(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("PrintText", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> PrintText(
            PrintFormat printFormat,
            bool printToScreen,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>(
                printToScreen ? $"PrintText({printFormat},String)" : $"PrintText({printFormat})",
                null,
                cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> PrintText(
            PrintFormat printFormat,
            string file,
            bool append = false,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var contentHandling = append ? "append" : "replace";
            return this.Request<string>(
                $"PrintText({printFormat},{contentHandling},file,{file})",
                null,
                cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Query(
            QueryKeyword keyword = QueryKeyword.None,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>(
                keyword == QueryKeyword.None ? "Query" : $"Query({keyword})",
                null,
                cancellationToken);
        }

        /// <summary>
        /// Exit <c>x3270</c>.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Quit(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Quit", null, cancellationToken);
        }

        /// <summary>
        /// Dumps the contents of the screen buffer, one line at a time. Positions inside data fields are generally output as 2-digit hexadecimal codes in the current display character set. If the current locale specifies UTF-8 (or certain DBCS character sets), some positions may be output as multi-byte strings (4-, 6- or 8-digit codes). DBCS characters take two positions in the screen buffer; the first location is output as a multi-byte string in the current locale code set, and the second location is output as a dash. Start-of-field characters (each of which takes up a display position) are output as <c>SF(aa=nn[,...])</c>, where <c>aa</c> is a field attribute type and <c>nn</c> is its value.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> ReadBufferAsAscii(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("ReadBuffer(Ascii)", null, cancellationToken);
        }

        /// <summary>
        /// Equivalent to <c>ReadBuffer(Ascii)</c>, but with the data fields output as hexadecimal EBCDIC codes instead. Additionally, if a buffer position has the Graphic Escape attribute, it is displayed as GE(xx).
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> ReadBufferAsEbcdic(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("ReadBuffer(Ebcdic)", null, cancellationToken);
        }

        /// <summary>
        /// Redraws the window.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Redraw(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Redraw", null, cancellationToken);
        }

        /// <summary>
        /// Resets locked keyboards.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Reset(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Reset", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor right.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Right(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Right", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor right 2 positions.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Right2(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Right2", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Script(
            string file,
            string[] args = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return (args == null)
                       ? this.Request<string>($"Script({file})", null, cancellationToken)
                       : this.Request<string>($"Script({file},{string.Join(",", args)})", null, cancellationToken);
        }

        /// <summary>
        /// Scroll screen forward.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> ScrollForward(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Scroll(Forward)", null, cancellationToken);
        }

        /// <summary>
        /// Scroll screen backward.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> ScrollBackward(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Scroll(Backward)", null, cancellationToken);
        }

        /// <summary>
        /// Saves a copy of the screen image and status in a temporary buffer. This copy can be queried with other Snap actions to allow a script to examine a consistent screen image, even when the host may be changing the image (or even the screen dimensions) dynamically. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Snap(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap", null, cancellationToken);
        }

        /// <summary>
        /// Performs the <c>Ascii</c> action on the saved screen image.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> SnapAscii(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap(Ascii)", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> SnapAscii(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Snap(Ascii,{row},{col},{rows},{cols})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> SnapAscii(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Snap(Ascii,{row},{col},{length})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> SnapAscii(
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Snap(Ascii,{length})", null, cancellationToken);
        }

        /// <summary>
        /// Returns the number of columns in the saved screen image. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> SnapCols(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap(Cols)", null, cancellationToken);
        }

        /// <summary>
        /// Performs the <c>Ebcdic</c> action on the saved screen image.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> SnapEbcdic(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap(Ebcdic)", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> SnapEbcdic(
            int row,
            int col,
            int rows,
            int cols,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Snap(Ebcdic,{row},{col},{rows},{cols})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> SnapEbcdic(
            int row,
            int col,
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Snap(Ebcdic,{row},{col},{length})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> SnapEbcdic(
            int length,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Snap(Ebcdic,{length})", null, cancellationToken);
        }

        /// <summary>
        /// Returns the number of rows in the saved screen image.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> SnapRows(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap(Rows)", null, cancellationToken);
        }

        /// <summary>
        /// Performs the ReadBuffer action on the saved screen image. 
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> SnapReadBuffer(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap(ReadBuffer)", null, cancellationToken);
        }

        /// <summary>
        /// Returns the status line from when the screen was last saved.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> SnapStatus(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Snap(Status)", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Source(
            string file,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Source({file})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Title(
            string text,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Title({text})", null, cancellationToken);
        }

        /// <summary>
        /// Move cursor to next input field.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Tab(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Tab", null, cancellationToken);
        }

        /// <summary>
        /// Send Enter command.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Enter(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Enter", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Key(
            char key,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"Key({key})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Pf(
            int programFunctionKey,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>($"PF({programFunctionKey})", null, cancellationToken);
        }

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
        public Task<StatusTextResponse<string>> Raw(
            string rawCommand,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>(rawCommand, null, cancellationToken);
        }

        /// <summary>
        /// Move cursor up.
        /// </summary>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<StatusTextResponse<string>> Up(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.Request<string>("Up", null, cancellationToken);
        }

        /// <summary>
        /// Sends a HTTP request.
        /// </summary>
        /// <param name="resource">
        /// The REST resource to use relative to the REST root.
        /// </param>
        /// <param name="parameters">
        /// The parameters for the request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <typeparam name="T">
        /// The return type of <see cref="IResponse{T}"/> Payload.
        /// </typeparam>
        /// <returns>
        /// An <see cref="IResponse{T}"/> object.
        /// </returns>
        private async Task<StatusTextResponse<string>> Request<T>(
            string resource,
            Dictionary<string, object> parameters = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            cancellationToken.ThrowIfCancellationRequested();
            var uri = new Uri(this.HostAddress + ":" + this.Port + RestRoot + HttpUtility.UrlEncode(resource));

            HttpResponseMessage response = new HttpResponseMessage();
            var responseStr = string.Empty;

            try
            {
                var query = HttpUtility.ParseQueryString(string.Empty);
                if (parameters != null)
                {
                    foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
                    {
                        if (parameter.Value is FileToSend) continue;
                        var content = HttpContentConverter.ConvertParameterValue(parameter.Value);
                        query[parameter.Key] = await content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }

                string queryString = HttpContentConverter.ConstructQueryString(query);
                response =
                    await this.httpClient.GetAsync(uri + "?" + queryString, cancellationToken).ConfigureAwait(false);

                responseStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                HtmlResponse htmlErrorResponse;
                if (HtmlResponse.TryParseError(responseStr, out htmlErrorResponse)) throw new RequestException(htmlErrorResponse.ErrorText, (int)response.StatusCode);
                throw new RequestException("The server answered unexpected.", (int)response.StatusCode, e);
            }
            catch (OperationCanceledException e)
            {
                // https://blogs.msdn.microsoft.com/andrewarnottms/2014/03/19/recommended-patterns-for-cancellationtoken/
                // includes TaskCanceledException
                if (cancellationToken.IsCancellationRequested) throw new RequestException("Cancellation was requested.", 0, e);
                throw new RequestException("Request timed out", 408, e);
            }

            /* Create the response */
            return this.ConstructStatusTextResponse(responseStr);
        }

        /// <summary>
        /// Constructs the status text response.
        /// </summary>
        /// <param name="input">The string to pack into new structure.</param>
        /// <returns>A <see cref="StatusTextResponse{T}"/> acquired from the x3270 terminal.</returns>
        /// <exception cref="RequestException">
        /// Server returned empty body.
        /// or
        /// Server returned mismatching status line.
        /// </exception>
        private StatusTextResponse<string> ConstructStatusTextResponse(string input)
        {
            /* ensure at least one line from server response is present */
            if (string.IsNullOrEmpty(input)) throw new RequestException("Server returned empty body.");

            var responseArray = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var statusFields = responseArray[0].Split(' ');

            if (statusFields.Length != 12)
                throw new RequestException(
                    $"Server returned mismatching status line. Length of status line is {statusFields.Length}");

            var response = new StatusTextResponse<string>
                               {
                                   NumberOfRows =
                                       int.Parse(
                                           statusFields[(int)StatusLineField.NumberOfRows]),
                                   NumberOfColumns =
                                       int.Parse(
                                           statusFields[(int)StatusLineField.NumberOfColumns]),
                                   CursorRow =
                                       int.Parse(
                                           statusFields[(int)StatusLineField.CursorRow]),
                                   CursorColumn =
                                       int.Parse(
                                           statusFields[(int)StatusLineField.CursorColumn]),
                                   ScreenFormatting =
                                       statusFields[(int)StatusLineField.ScreenFormatting],
                                   KeyboardState =
                                       statusFields[(int)StatusLineField.KeyboardState],
                                   FieldProtection =
                                       statusFields[(int)StatusLineField.FieldProtection],
                                   ConnectionState =
                                       statusFields[(int)StatusLineField.ConnectionState],
                                   EmulatorMode =
                                       statusFields[(int)StatusLineField.EmulatorMode],
                                   ModelNumber =
                                       int.Parse(
                                           statusFields[(int)StatusLineField.ModelNumber]),
                                   WindowId = statusFields[(int)StatusLineField.WindowId]
                               };
            double executionTime;
            response.CommandExecutionTime = double.TryParse(
                statusFields[(int)StatusLineField.CommandExecutionTime],
                out executionTime)
                                                ? TimeSpan.FromSeconds(executionTime)
                                                : TimeSpan.Zero;
            response.PayLoad = (responseArray.Length > 1)
                                   ? string.Join(Environment.NewLine, responseArray.Skip(1))
                                   : string.Empty;

            /* everything went fine todo: check for conditions that lead to false. if there aren't any, remove the property. */
            response.Success = true;
            return response;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        /// <param name="disposing">
        /// Indicates if other managed resources should be disposed.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (this.disposed) return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
                this.httpClient.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null
            this.disposed = true;
        }
    }
}