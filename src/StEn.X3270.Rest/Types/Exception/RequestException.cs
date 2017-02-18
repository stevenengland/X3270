// ReSharper disable UnusedMember.Global because some members are exposed for extended API usage
namespace StEn.X3270.Rest.Types.Exception
{
    using Exception = System.Exception;

    /// <summary>
    /// Represents an exception that occur when sending requests to the x3270 REST API.
    /// </summary>
    /// <seealso cref="Exception" />
    public class RequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// Used when no HTTP status can be determined yet. HttpStatusCode is 0 then.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RequestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="httpStatusCode">The HttpStatusCode.</param>
        public RequestException(string message, int httpStatusCode)
            : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="errorCode">The internal error code.</param>
        public RequestException(string message, int httpStatusCode, int errorCode)
            : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public RequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public RequestException(string message, int httpStatusCode, Exception innerException)
            : base(message, innerException)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="httpStatusCode">The mapped external error code.</param>
        /// <param name="errorCode">The HTTP status code</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public RequestException(string message, int httpStatusCode, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            this.HttpStatusCode = httpStatusCode;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets the mapped HTTP error code.
        /// </summary>
        public int HttpStatusCode { get; private set; }

        /// <summary>
        /// Gets the mapped external error code.
        /// </summary>
        public int ErrorCode { get; private set; }
    }
}