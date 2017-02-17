namespace StEn.X3270.Rest.Test
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using StEn.X3270.Rest.Extensions;
    using StEn.X3270.Rest.StatusText;
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

            string serverAppPath = this.pathToMock + "sim390\\sim390_17.exe";
            string clientAppPath = this.pathToMock + "x3270\\wc3270.exe";

            if (!File.Exists(serverAppPath) || !File.Exists(clientAppPath)) throw new FileNotFoundException("Either server or client app not found.");

            /* create the server and terminal to interact with */
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

            this.server = Process.Start(serverStartInfo);
            Thread.Sleep(5000); // not really important, more for visual inspections in attended tests
            this.client = Process.Start(clientStartInfo);
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
        public void TestMacroAndQuit()
        {
            var ex = Assert.ThrowsAsync<RequestException>(async () => await this.apiClient.Macro("testmacro").ConfigureAwait(false));
            Assert.That(ex.Message == "no such macro: 'testmacro'");
            Assert.ThrowsAsync<RequestException>(async () => await this.apiClient.Quit().ConfigureAwait(false));
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
            await this.apiClient.Keymap().ConfigureAwait(false);
            var ex = Assert.ThrowsAsync<RequestException>(async () => await this.apiClient.Keymap("test").ConfigureAwait(false));
            Assert.That(ex.Message == "no such keymap: 'test'");
        }

        /// <summary>
        /// Run commands but don't check the result. It is only important that no error returns what means the command was accepted anyhow.
        /// Used because there is too less time to test each and every command ;)
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task CommandWithoutExpectedResult()
        {
            await this.apiClient.Raw("Ascii").ConfigureAwait(false);
            await this.apiClient.ReadBufferAsAscii().ConfigureAwait(false);
            await this.apiClient.ReadBufferAsEbcdic().ConfigureAwait(false);
            await this.apiClient.Redraw().ConfigureAwait(false);
            await this.apiClient.Reset().ConfigureAwait(false);
            await this.apiClient.Ascii(0, 0, 10, 10).ConfigureAwait(false);
            await this.apiClient.Ascii(0, 0, 10).ConfigureAwait(false);
            await this.apiClient.Ascii(10).ConfigureAwait(false);
            await this.apiClient.Ebcdic(0, 0, 10, 10).ConfigureAwait(false);
            await this.apiClient.Ebcdic(0, 0, 10).ConfigureAwait(false);
            await this.apiClient.Ebcdic(10).ConfigureAwait(false);
            await this.apiClient.AnsiText().ConfigureAwait(false);
            await this.apiClient.Query().ConfigureAwait(false);
            await this.apiClient.Query(QueryKeyword.BindPluName).ConfigureAwait(false);
            await this.apiClient.ScrollForward().ConfigureAwait(false);
            await this.apiClient.ScrollBackward().ConfigureAwait(false);



            await this.apiClient.CircumNot().ConfigureAwait(false);
            await this.apiClient.BackSpace().ConfigureAwait(false);

            await this.apiClient.CursorSelect().ConfigureAwait(false);
            await this.apiClient.BackTab().ConfigureAwait(false);
            await this.apiClient.Tab().ConfigureAwait(false);

            await this.apiClient.Home().ConfigureAwait(false);
            await this.apiClient.Compose().ConfigureAwait(false);  
            await this.apiClient.Key('C').ConfigureAwait(false);
            await this.apiClient.Key(',').ConfigureAwait(false);
            await this.apiClient.Delete().ConfigureAwait(false);
            await this.apiClient.DeleteField().ConfigureAwait(false);
            await this.apiClient.DeleteWord().ConfigureAwait(false);
            await this.apiClient.Cut().ConfigureAwait(false);
            await this.apiClient.Clear().ConfigureAwait(false);
            await this.apiClient.Down().ConfigureAwait(false);
            await this.apiClient.Up().ConfigureAwait(false);
            await this.apiClient.Home().ConfigureAwait(false);
            await this.apiClient.Dup().ConfigureAwait(false);
            await this.apiClient.Ebcdic().ConfigureAwait(false);
            await this.apiClient.EbcdicField().ConfigureAwait(false);
            await this.apiClient.Erase().ConfigureAwait(false);
            await this.apiClient.EraseEof().ConfigureAwait(false);
            await this.apiClient.EraseInput().ConfigureAwait(false);
            await this.apiClient.FieldEnd().ConfigureAwait(false);
            await this.apiClient.FieldMark().ConfigureAwait(false);
            await this.apiClient.Info("test").ConfigureAwait(false);
            await this.apiClient.Ignore().ConfigureAwait(false);
            await this.apiClient.Home().ConfigureAwait(false);
            await this.apiClient.Left().ConfigureAwait(false);
            await this.apiClient.Left2().ConfigureAwait(false);
            await this.apiClient.Right().ConfigureAwait(false);
            await this.apiClient.Right2().ConfigureAwait(false);

            await this.apiClient.MoveCursor().ConfigureAwait(false);
            await this.apiClient.MoveCursor(0, 0).ConfigureAwait(false);

            await this.apiClient.Home().ConfigureAwait(false);
            await this.apiClient.Clear().ConfigureAwait(false);
            await this.apiClient.Newline().ConfigureAwait(false);
            await this.apiClient.NextWord().ConfigureAwait(false);
            await this.apiClient.PreviousWord().ConfigureAwait(false);
            await this.apiClient.Title("new title").ConfigureAwait(false);

            await this.apiClient.Home().ConfigureAwait(false);
            await this.apiClient.Clear().ConfigureAwait(false);
            await this.apiClient.Execute("echo hallo").ConfigureAwait(false);


            await this.apiClient.MonoCase().ConfigureAwait(false);
            await this.apiClient.MonoCase().ConfigureAwait(false);

            await this.apiClient.Attn().ConfigureAwait(false);

            await this.apiClient.HexString("FF").ConfigureAwait(false);
            await this.apiClient.Source("inputSource.txt").ConfigureAwait(false); // assumes txt insame directory as x3270
            /* will disconnect AND and return a status */
            await this.apiClient.Disconnect().ConfigureAwait(false);
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


    }
}