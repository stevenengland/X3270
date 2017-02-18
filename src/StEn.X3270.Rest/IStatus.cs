// ReSharper disable UnusedMemberInSuper.Global
namespace StEn.X3270.Rest
{
    /// <summary>
    /// Interface for application status information.
    /// </summary>
    public interface IStatus
    {
        /// <summary>
        /// Gets a value indicating whether the operation succeeded.
        /// </summary>
        bool Success { get; }
    }
}