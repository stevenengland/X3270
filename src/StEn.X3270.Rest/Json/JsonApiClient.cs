namespace StEn.X3270.Rest.Json
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using StatusText;
    using Types.Enums;

    /// <summary>
    /// This is just a dummy at the moment due to the fact that the JSON interface is not present yet in th x3270 client. Therefore it is made internal
    /// </summary>
    internal class JsonApiClient : IApiClient, IDisposable
    {
        public int Port { get; }

        public string HostAddress { get; }

        public Task<HtmlResponse> CheckConnectionStatus(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> AnsiText(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ascii(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ascii(int row, int col, int rows, int cols, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ascii(int row, int col, int length, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ascii(int length, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> AsciiField(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Attn(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> BackSpace(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> BackTab(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> CircumNot(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Clear(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Compose(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> CursorSelect(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Cut(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Delete(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> DeleteField(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> DeleteWord(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Disconnect(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Down(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Dup(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ebcdic(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ebcdic(int row, int col, int rows, int cols, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ebcdic(int row, int col, int length, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ebcdic(int length, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> EbcdicField(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Ignore(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Key(char key, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Left(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Left2(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Macro(string macroName, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> MonoCase(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> MoveCursor(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> MoveCursor(int row, int col, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Newline(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> NextWord(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> PreviousWord(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Pf(int programFunctionKey, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Quit(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> ReadBufferAsAscii(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> ReadBufferAsEbcdic(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Redraw(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Reset(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Right(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Right2(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Raw(string rawCommand, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Source(string file, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Title(string text, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Tab(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Up(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Enter(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Erase(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> EraseEof(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> EraseInput(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Execute(string command, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> FieldEnd(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> FieldMark(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> HexString(string hex, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Home(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Info(string message, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<StatusTextResponse<string>> Insert(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public bool CancelRequested { get; set; }

        public IWebProxy WebProxy { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
