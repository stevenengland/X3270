namespace StEn.X3270.Rest
{
    using System.Text;

    /// <summary>
    /// Interface for application status information.
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the encoding of the result.
        /// </summary>
        Encoding Encoding { get; }
    }
}
