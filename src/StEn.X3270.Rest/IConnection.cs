// ReSharper disable UnusedMemberInSuper.Global
namespace StEn.X3270.Rest
{
    using System.Threading;
    using System.Threading.Tasks;

    using StEn.X3270.Rest.Types.Enums;

    /// <summary>
    /// Defines the interface for all REST based connections.
    /// </summary>
    internal interface IConnection
    {
        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        int Port { get; }

        /// <summary>
        /// Gets the rest root.
        /// </summary>
        /// <value>
        /// The rest root.
        /// </value>
        string HostAddress { get; }

        /// <summary>
        /// Checks whether the connection is established or not.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><c>true</c> if the connection is established.</returns>
        Task<HtmlResponse> CheckConnectionStatus(CancellationToken cancellationToken = default(CancellationToken));
    }
}