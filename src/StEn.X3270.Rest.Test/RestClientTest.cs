namespace StEn.X3270.Rest.Test
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using StEn.X3270.Rest.Extensions;
    using StEn.X3270.Rest.StatusText;
    using StEn.X3270.Rest.Types;
    using StEn.X3270.Rest.Types.Enums;
    using StEn.X3270.Rest.Types.Exception;

    /// <summary>
    /// Tests concerning the rest client.
    /// </summary>
    [TestFixture]
    public class RestClientTest
    {
        /// <summary>
        /// Path to mocking toolset directory
        /// </summary>
        private readonly string pathToMock =
            TryGetSolutionDirectoryInfo(Path.GetDirectoryName(Assembly.GetAssembly(typeof(RestClientTest)).Location))
                .FullName + "\\..\\MockServer\\";

        /// <summary>
        /// The API client.
        /// </summary>
        private readonly StatusTextApiClient apiClient = new StatusTextApiClient("http://localhost", 6001);

        /// <summary>
        /// Is the server process to connect to via telnet client x3270.
        /// </summary>
        private Process server = new Process();

        /// <summary>
        /// Is the client (x3270) process.
        /// </summary>
        private Process client = new Process();

        #region SetupAndTeardown

        /// <summary>
        /// Sets up the environment for all tests.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            if (!Directory.Exists(this.pathToMock)) throw new DirectoryNotFoundException();

            var serverAppPath = this.pathToMock + "sim390\\sim390_17.exe";
            var clientAppPath = this.pathToMock + "x3270\\wc3270.exe";

            if (!File.Exists(serverAppPath) || !File.Exists(clientAppPath)) throw new FileNotFoundException("Either server or client app not found.");

            /* create the server and terminal to interact with */
            this.StartServer(serverAppPath);
            Thread.Sleep(5000); // not really important, more for visual inspections in attended tests
            this.StartClient(clientAppPath);
        }

        /// <summary>
        /// The tear down after each test.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [TearDown]
        public async Task TearDownEach()
        {
            /* Determine if we are on the start screen */
            var response = await this.apiClient.Ascii().ConfigureAwait(false);
            if (response.PayLoad.To2DimensionalTerminalArray()[15][10] == "u") return;

            /* If not navigate to start screen */
            await this.apiClient.Tab().ConfigureAwait(false); // If keys are locked
            await this.apiClient.Pf(3).ConfigureAwait(false); // log off

            /* Determine if we are on the start screen */
            response = await this.apiClient.Ascii().ConfigureAwait(false);
            Assert.That(response.PayLoad.To2DimensionalTerminalArray()[15][10] == "u");
        }

        /// <summary>
        /// Tear Down functionality.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            /* shutdown server and terminal instances */
            if (this.client != null)
            {
                try
                {
                    this.client.Kill();
                }
                catch
                {
                    // ignored
                }

                this.client.Dispose();
                this.client = null;
            }

            if (this.server == null)
            {
                return;
            }

            try
            {
                this.server.Kill();
            }
            catch
            {
                // ignored
            }

            this.server.Dispose();
            this.server = null;
        }

        #endregion

        /// <summary>
        /// Test output of the status line.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Status")]
        public async Task TestStatusLine()
        {
            var response = await this.apiClient.Ascii().ConfigureAwait(false);
            Assert.True(
                !string.IsNullOrEmpty(response.KeyboardState) && !string.IsNullOrEmpty(response.ScreenFormatting)
                && !string.IsNullOrEmpty(response.FieldProtection) && !string.IsNullOrEmpty(response.ConnectionState)
                && !string.IsNullOrEmpty(response.EmulatorMode)
                && (response.ModelNumber > 0 && response.ModelNumber < 6) && response.NumberOfColumns > 0
                && response.NumberOfRows > -1 && response.CursorRow > -1 && response.CursorColumn > -1
                && !string.IsNullOrEmpty(response.WindowId) && response.CommandExecutionTime == TimeSpan.Zero);
        }

        /// <summary>
        /// Test the connectivity routine.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Status")]
        public async Task TestConnectionCheck()
        {
            var response = await this.apiClient.CheckConnectionStatus().ConfigureAwait(false);
            Assert.True(response.ErrorText == string.Empty);
        }

        /// <summary>
        /// Test the <c>Ascii</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestAscii()
        {
            var response = await this.apiClient.Ascii().ConfigureAwait(false);
            Assert.That(response.PayLoad.To2DimensionalTerminalArray()[15][10] == "u");
        }

        /// <summary>
        /// Test the <c>AsciiField</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestAsciiField()
        {
            await this.apiClient.Enter().ConfigureAwait(false);
            await this.apiClient.Key('+').ConfigureAwait(false);
            var response = await this.apiClient.AsciiField().ConfigureAwait(false);
            Assert.That(response.PayLoad.StartsWith("+"));
        }

        /// <summary>
        /// Test the <c>Key</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestKey()
        {
            await this.apiClient.Enter().ConfigureAwait(false);
            await this.apiClient.Key('+').ConfigureAwait(false);
            var response = await this.apiClient.AsciiField().ConfigureAwait(false);
            Assert.That(response.PayLoad.StartsWith("+"));
        }

        /// <summary>
        /// Test the <c>Tab</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestTab()
        {
            await this.apiClient.Enter().ConfigureAwait(false);
            var response = await this.apiClient.Tab().ConfigureAwait(false);
            var currentCursorRow = response.CursorRow;
            response = await this.apiClient.Tab().ConfigureAwait(false);
            Assert.AreNotEqual(currentCursorRow, response.CursorRow);
        }

        /// <summary>
        /// Test the <c>Enter</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestEnter()
        {
            /* Determine if we are on the start screen */
            var response = await this.apiClient.Ascii().ConfigureAwait(false);
            Assert.That(response.PayLoad.To2DimensionalTerminalArray()[15][10] == "u");
            await this.apiClient.Enter().ConfigureAwait(false);
            response = await this.apiClient.Ascii().ConfigureAwait(false);
            Assert.That(response.PayLoad.To2DimensionalTerminalArray()[15][10] != "u");
        }

        /// <summary>
        /// Test the <c>Macro</c> command.
        /// </summary>
        [Test, Category("Rest Actions")]
        public void TestMacro()
        {
            var ex =
                Assert.ThrowsAsync<RequestException>(
                    async () => await this.apiClient.Macro("testmacro").ConfigureAwait(false));
            Assert.That(ex.Message == "no such macro: 'testmacro'");
            Assert.That(ex.HttpStatusCode > 399);
            Assert.That(ex.ErrorCode == 0);
        }

        /// <summary>
        /// Test the <c>Quit</c> command.
        /// </summary>
        [Test, Category("Rest Actions")]
        public void TestQuit()
        {
            Assert.ThrowsAsync<RequestException>(async () => await this.apiClient.Quit().ConfigureAwait(false));
            this.StartClient(this.pathToMock + "x3270\\wc3270.exe");
        }

        /// <summary>
        /// Test the <c>Disconnect</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestDisconnect()
        {
            /* will disconnect AND and return a status */
            await this.apiClient.Disconnect().ConfigureAwait(false);
            this.StartClient(this.pathToMock + "x3270\\wc3270.exe");
        }

        /// <summary>
        /// Test the <c>Macro</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestKeymap()
        {
            var response = await this.apiClient.Keymap().ConfigureAwait(false);
            Assert.That(response.Success);
            var ex =
                Assert.ThrowsAsync<RequestException>(
                    async () => await this.apiClient.Keymap("test").ConfigureAwait(false));
            Assert.That(ex.Message == "No such keymap resource or file: test");
            Assert.That(ex.HttpStatusCode > 399);
            Assert.That(ex.ErrorCode == 0);
        }

        /// <summary>
        /// Test the <c>PrintText</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestPrintText()
        {
            await this.apiClient.PrintText(PrintFormat.Html, true).ConfigureAwait(false);
            await this.apiClient.PrintText(PrintFormat.Rtf, true).ConfigureAwait(false);
            await this.apiClient.PrintText(PrintFormat.Modi, true).ConfigureAwait(false);
            await this.apiClient.PrintText(PrintFormat.Html, "testPrinting.txt", true).ConfigureAwait(false);
            await this.apiClient.PrintText(PrintFormat.Html, "testPrinting.txt").ConfigureAwait(false);
        }

        /// <summary>
        /// Test the <c>Query</c> command.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task TestQuery()
        {
            await this.apiClient.Query().ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.BindPluName).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.ConnectionState).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.CodePage).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.Cursor).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.Formatted).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.Host).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.LocalEncoding).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.LuName).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.Model).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.ScreenCurSize).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.ScreenMaxSize).ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.Ssl).ConfigureAwait(false);
        }

        /// <summary>
        /// Run commands but don't check the result. It is only important that no error returns what means the command was accepted anyhow.
        /// Used because there is too less time to test each and every command ;)
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Test, Category("Rest Actions")]
        public async Task CommandWithoutExpectedResult()
        {
            var response = await this.apiClient.Script("testScript.bat").ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Script("testScript.bat", new[] { "hallo", "test" }).ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Raw("Ascii").ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.ReadBufferAsAscii().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.ReadBufferAsEbcdic().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Redraw().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Reset().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ascii(0, 0, 10, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ascii(0, 0, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ascii(10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ebcdic(0, 0, 10, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ebcdic(0, 0, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ebcdic(10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.AnsiText().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.ScrollForward().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.ScrollBackward().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Snap().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapAscii().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapAscii(3, 3, 10, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapAscii(3, 3, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapAscii(3).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapEbcdic().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapEbcdic(3, 3, 10, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapEbcdic(3, 3, 10).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapEbcdic(3).ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapCols().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapRows().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapReadBuffer().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.SnapStatus().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.Enter().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.CircumNot().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.BackSpace().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.CursorSelect().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.BackTab().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Tab().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.Home().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Compose().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Key('C').ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            //response = await this.apiClient.Key(',').ConfigureAwait(false);
            //Assert.That(string.IsNullOrEmpty(response.PayLoad));
            //response = await this.apiClient.Clear().ConfigureAwait(false);
            response = await this.apiClient.Delete().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.DeleteField().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.DeleteWord().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Cut().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Clear().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Down().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Up().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Home().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Dup().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ebcdic().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.EbcdicField().ConfigureAwait(false);
            Assert.That(!string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Erase().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.EraseEof().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.EraseInput().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.FieldEnd().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.FieldMark().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Info("test").ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Ignore().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Home().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Left().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Left2().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Right().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Right2().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.MoveCursor(0, 0).ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.Home().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Clear().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Newline().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.NextWord().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.PreviousWord().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Title("new title").ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.Home().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Clear().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Execute("echo hallo").ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.MonoCase().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.MonoCase().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.Attn().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.HexString("FF").ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
            response = await this.apiClient.Source("testSource.txt").ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));

            response = await this.apiClient.Insert().ConfigureAwait(false);
            Assert.That(string.IsNullOrEmpty(response.PayLoad));
        }

        /// <summary>
        /// Test uploading of files.
        /// </summary>
        [Test, Category("Rest Actions")]
        public void TestFileUpload()
        {
            using (var fileStream = new FileStream(this.pathToMock + "x3270\\test.txt", FileMode.Open))
            {
                var fts1 = new FileToSend("test.txt", fileStream);
                var filename = fts1.Filename; // for later uploads
                Assert.That(filename == "test.txt");
                Assert.AreEqual(fts1.Type, FileType.Stream);
            }

            var fts2 = new FileToSend(new Uri("https://github.com/stevenengland/X3270/master/newFile.png"));
            Assert.AreEqual(fts2.Type, FileType.Url);

            var fts3 = new FileToSend("https://github.com/stevenengland/X3270/master/abcdefg-hijkl-mnop");
            var fileId = fts3.FileId; // for later uploads
            Assert.That(fileId == "https://github.com/stevenengland/X3270/master/abcdefg-hijkl-mnop");
            Assert.AreEqual(fts3.Type, FileType.Id);

            // lacks real uploading at the moment
        }

        /// <summary>
        /// Tries to find the project solution file.
        /// </summary>
        /// <param name="currentPath">
        /// The current path.
        /// </param>
        /// <returns>
        /// The <see cref="DirectoryInfo"/>.
        /// </returns>
        private static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory;
        }

        /// <summary>
        /// Starts the x3270 client.
        /// </summary>
        /// <param name="clientAppPath">
        /// The client app path.
        /// </param>
        private void StartClient(string clientAppPath)
        {
            var clientStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                Arguments = "-httpd 6001 +S \"test.wc3270\"",
                FileName = clientAppPath,
                RedirectStandardError = false,
                RedirectStandardOutput = false,
                UseShellExecute = true,

                // ReSharper disable once AssignNullToNotNullAttribute
                WorkingDirectory = Path.GetDirectoryName(clientAppPath)
            };

            this.client = Process.Start(clientStartInfo);
        }

        /// <summary>
        /// Start the <c>Sim390</c> server.
        /// </summary>
        /// <param name="serverAppPath">
        /// The server app path.
        /// </param>
        private void StartServer(string serverAppPath)
        {
            var serverStartInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                Arguments = "config.txt", // no double quotes allowed
                FileName = serverAppPath,
                RedirectStandardError = false,
                RedirectStandardOutput = false,
                UseShellExecute = false,

                // ReSharper disable once AssignNullToNotNullAttribute
                WorkingDirectory = Path.GetDirectoryName(serverAppPath)
            };

            this.server = Process.Start(serverStartInfo);
        }
    }
}