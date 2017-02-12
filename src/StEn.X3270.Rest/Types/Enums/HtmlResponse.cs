namespace StEn.X3270.Rest.Types.Enums
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Covers HTML based responses from x3270 client.
    /// </summary>
    public class HtmlResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlResponse"/> class.
        /// </summary>
        /// <param name="errorText">
        /// The error text.
        /// </param>
        public HtmlResponse(string errorText)
        {
            this.ErrorText = errorText;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="HtmlResponse"/> class from being created. 
        /// Initializes a new instance of the <see cref="HtmlResponse"/> class.
        /// </summary>
        private HtmlResponse()
        {
        }

        /// <summary>
        /// Gets the status text.
        /// </summary>
        public string StatusText { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the error text.
        /// </summary>
        public string ErrorText { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the info text.
        /// </summary>
        public string InfoText { get; private set; } = string.Empty;

        /// <summary>
        /// Tries to parse an error page returned by x3270 client.
        /// </summary>
        /// <param name="html">
        /// The HTML string.
        /// </param>
        /// <param name="htmlErrorResponse">
        /// The HTML error response to set.
        /// </param>
        /// <returns>
        /// <c>true</c> if parsing was successful.
        /// </returns>
        public static bool TryParseError(string html, out HtmlResponse htmlErrorResponse)
        {
            /* Only basic regex to avoid dependencies like Windows.Forms or HtmlAgilityPack */
            htmlErrorResponse = new HtmlResponse();

            Match m = Regex.Match(html, @"<title>\s*(.+?)\s*</title>");
            if (!m.Success) return false;
            htmlErrorResponse.StatusText = m.Groups[1].Value;

            m = Regex.Match(html, @"<i>\s*(.+?)\s*[-]");
            if (!m.Success) return false;
            htmlErrorResponse.InfoText = m.Groups[1].Value;

            m = Regex.Match(html, @"</h1>\s*(.+?)\s*<hr>");
            if (!m.Success) return false;
            htmlErrorResponse.ErrorText = m.Groups[1].Value;

            return true;
        }

        /// <summary>
        /// Tries to parse an standard page returned by x3270 client.
        /// </summary>
        /// <param name="html">
        /// The HTML string.
        /// </param>
        /// <param name="htmlErrorResponse">
        /// The HTML response to set.
        /// </param>
        /// <returns>
        /// <c>true</c> if parsing was successful.
        /// </returns>
        public static bool TryParseRestRoot(string html, out HtmlResponse htmlErrorResponse)
        {
            /* Only basic regex to avoid dependencies like Windows.Forms or HtmlAgilityPack */
            htmlErrorResponse = new HtmlResponse();

            var m = Regex.Match(html, @"<title>\s*(.+?)\s*</title>");
            if (!m.Success) return false;
            htmlErrorResponse.StatusText = m.Groups[1].Value;

            m = Regex.Match(html, @"<i>\s*(.+?)\s*[-]");
            if (!m.Success) return false;
            htmlErrorResponse.InfoText = m.Groups[1].Value;

            return true;
        }
    }
}