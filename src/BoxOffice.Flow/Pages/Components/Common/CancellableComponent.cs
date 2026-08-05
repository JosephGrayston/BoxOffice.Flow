using Microsoft.AspNetCore.Components;

namespace BoxOffice.Flow.Pages.Components.Common;

public abstract class CancellableComponent : ComponentBase, IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    protected CancellationToken CancellationToken => _cancellationTokenSource.Token;

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
