namespace StEn.X3270.Rest
{
    using System.Threading;
    using System.Threading.Tasks;

    using StatusText;

    internal interface IActions
    {
        Task<StatusTextResponse<string>> Ascii(CancellationToken cancellationToken = default(CancellationToken));

        Task<StatusTextResponse<string>> PF(int programFunctionKey, CancellationToken cancellationToken = default(CancellationToken));


        Task<StatusTextResponse<string>> Tab(CancellationToken cancellationToken = default(CancellationToken));

        Task<StatusTextResponse<string>> Enter(CancellationToken cancellationToken = default(CancellationToken));

    }
}
