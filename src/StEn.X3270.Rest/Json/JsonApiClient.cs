namespace StEn.X3270.Rest.Json
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using StatusText;
    using Types.Enums;

    /// <summary>
    /// This is just a dummy at the moment due to the fact that the JSON interface is not present yet in th x3270 client.
    /// </summary>
    public class JsonApiClient : IApiClient, IDisposable
    {
        public int Port { get; }

        public string HostAddress { get; }

        public Task<HtmlResponse> CheckConnectionStatus(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ascii(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> PF(int programFunctionKey, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Tab(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Enter(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public bool CancelRequested { get; set; }

        public IWebProxy WebProxy { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
