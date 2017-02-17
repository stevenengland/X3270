namespace StEn.X3270.Rest.Types.Enums
{
    /// <summary>
    /// Query keywords.
    /// </summary>
    public enum QueryKeyword
    {
        /// <summary>
        /// No specified keyword
        /// </summary>
        None,

        /// <summary>
        /// BIND PLU returned by the host
        /// </summary>
        BindPluName,

        /// <summary>
        /// <c>TN3270/TN3270E</c> mode and sub-mode
        /// </summary>
        ConnectionState,

        /// <summary>
        /// Host code page
        /// </summary>
        CodePage,

        /// <summary>
        /// Cursor position (row col)
        /// </summary>
        Cursor,

        /// <summary>
        /// 3270 format state (formatted or unformatted)
        /// </summary>
        Formatted,

        /// <summary>
        /// Host name and port
        /// </summary>
        Host,

        /// <summary>
        /// Local character encoding
        /// </summary>
        LocalEncoding,

        /// <summary>
        /// Host name LU name
        /// </summary>
        LuName,

        /// <summary>
        /// 3270 model name (IBM-327x-n)
        /// </summary>
        Model,

        /// <summary>
        /// Current screen size (rows cols)
        /// </summary>
        ScreenCurSize,

        /// <summary>
        /// Maximum screen size (rows cols)
        /// </summary>
        ScreenMaxSize,

        /// <summary>
        /// SSL state (secure or not-secure) and host validation state (host-verified or host-unverified)
        /// </summary>
        Ssl
    }
}