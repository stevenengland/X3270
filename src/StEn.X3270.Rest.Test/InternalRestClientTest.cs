namespace StEn.X3270.Rest.Test
{
    using NUnit.Framework;

    using Types.Enums;

    /// <summary>
    /// Internal unit tests of the <see cref="StatusText.StatusTextApiClient"/>
    /// </summary>
    [TestFixture]
    public class InternalRestClientTest
    {
        /// <summary>
        /// Test the correctness of parsed HTML error responses.
        /// </summary>
        [Test, Category("Internal RestClient")]
        public void ParseError()
        {
            const string ErrorHtml = @"
               <!DOCTYPE HTML PUBLIC ""-//IETF//DTD HTML 2.0//EN"">
                <html>
                 <head>
                  <title>400 Bad Request</title>
                 </head>
                 <body>
                 <h1>400 Bad Request</h1>
                Unknown action: Enters

                 <hr>
                 <i>wc3270 v3.5ga10 Mon Jan 16 17:54:50 CST 2017 pdm - <a href=""http://x3270.bgp.nu/"">x3270.bgp.nu</a></i>
                 </body>
                </html>
          ";
            HtmlResponse htmlErrorResponse;
            Assert.True(HtmlResponse.TryParseError(ErrorHtml, out htmlErrorResponse));
            Assert.That(htmlErrorResponse.StatusText == "400 Bad Request");
            Assert.That(htmlErrorResponse.InfoText == "wc3270 v3.5ga10 Mon Jan 16 17:54:50 CST 2017 pdm");
            Assert.That(htmlErrorResponse.ErrorText == "Unknown action: Enters");
        }

        /// <summary>
        /// Test the correctness of parsed HTML responses.
        /// </summary>
        [Test, Category("Internal RestClient")]
        public void ParseRestRoot()
        {
            const string RestRootHtml = @"
                <html>
                    <head>
                        <title>Directory of /3270/rest/</title>
                    </head>
                    <body>
                        <h1>Directory of /3270/rest/</h1>
                        <p>
                            <tt>
                                <a href=""/3270/rest/json"">json/</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </tt>REST JSON interface
                        </p>
                        <p>
                            <tt>
                                <a href=""html/Query()"">html/</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </tt>REST HTML interface
                        </p>
                        <p>
                            <tt>
                                <a href=""stext/Query()"">stext/</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </tt>REST plain text interface with status line
                        </p>
                        <p>
                            <tt>
                                <a href=""text/Query()"">text/</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </tt>REST plain text interface
                        </p>
                        <hr>
                        <i>wc3270 v3.5ga10 Mon Jan 16 17:54:50 CST 2017 pdm - 
                            <a href=""http://x3270.bgp.nu/"">x3270.bgp.nu</a>
                        </i>
                    </body>
                </html>
            ";
            HtmlResponse htmlResponse;
            Assert.True(HtmlResponse.TryParseRestRoot(RestRootHtml, out htmlResponse));
            Assert.That(htmlResponse.StatusText == "Directory of /3270/rest/");
            Assert.That(htmlResponse.InfoText == "wc3270 v3.5ga10 Mon Jan 16 17:54:50 CST 2017 pdm");
            Assert.That(htmlResponse.ErrorText == string.Empty);
        }
    }
}