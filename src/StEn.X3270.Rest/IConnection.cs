namespace StEn.X3270.Rest
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using StEn.X3270.Rest.Types.Enums;

    /// <summary>
    /// Defines the interface for all REST based connections.
    /// </summary>
    [ContractClass(typeof(ConnectionContract))]
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

    [ContractClassFor(typeof(IConnection))]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Only easy readable rules")]
    internal sealed class ConnectionContract : IConnection
    {
        private ConnectionContract()
        {
            // optional safeguard, prevent instantiation
        }

        public int Port
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);

                return default(int);
            }
        }

        public string HostAddress
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

                return default(string);
            }
        }

        public Task<HtmlResponse> CheckConnectionStatus(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotSupportedException();
        }
    }
}