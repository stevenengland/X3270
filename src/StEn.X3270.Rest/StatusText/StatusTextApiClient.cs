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
        /// Gets or sets a value indicating whether cancel is requested.
        /// </summary>
        public bool CancelRequested { get; set; }

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
        public Task<StatusTextResponse<string>> Key(char key, CancellationToken cancellationToken = default(CancellationToken))
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