namespace StEn.X3270.Rest
{
    using System.Net;

    /// <summary>
    /// The API client interface for the different REST endpoints.
    /// </summary>
    internal interface IApiClient : IConnection, IActions
    {
        /// <summary>
        /// Gets or sets a value indicating whether cancel is requested.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancel is requested; otherwise, <c>false</c>.
        /// </value>
        bool CancelRequested { get; set; }

        /// <summary>
        /// Gets the WebProxy for the HTTP client.
        /// </summary>
        /// <value>
        /// The web proxy.
        /// </value>
        IWebProxy WebProxy { get; }
    }
}