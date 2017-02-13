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
