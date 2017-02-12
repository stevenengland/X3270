namespace StEn.X3270.Rest
{
    /// <summary>
    /// The result of an request operation to the emulator.
    /// </summary>
    /// <typeparam name="T">Type of the payload that the response carries. 
    /// </typeparam>
    public interface IResponse<T> : IStatus, IStatusLine
    {
        /// <summary>
        /// Gets the output from the operation.
        /// </summary>
        T PayLoad { get; }
    }
}
