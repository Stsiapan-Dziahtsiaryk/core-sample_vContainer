using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.StateMachine
{
    public interface IAsyncState : IExitableState
    {
        Task EnterAsync(CancellationToken token);
    }
}