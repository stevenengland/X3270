// ReSharper disable UnusedMemberInSuper.Global
namespace StEn.X3270.Rest
{
    using System.Net;

    /// <summary>
    /// The API client interface for the different REST endpoints.
    /// </summary>
    internal interface IApiClient : IConnection, IActions
    {
        /// <summary>
        /// Gets the WebProxy for the HTTP client.
        /// </summary>
        /// <value>
        /// The web proxy.
        /// </value>
        IWebProxy WebProxy { get; }
    }
}