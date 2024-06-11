using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedSemaphoreSlim : ScopedPrimitive
    {
        private readonly SemaphoreSlim _semaphore;

        public ScopedSemaphoreSlim(int initialCount)
        {
            _semaphore = new SemaphoreSlim(initialCount);
        }

        public ScopedSemaphoreSlim(SemaphoreSlim semaphoreSlim)
        {
            _semaphore = semaphoreSlim;
        }

        public override SynchronizationScope CreateScope()
        {
            base.ThrowIfDisposed();
            _semaphore.Wait();
            return CreateNewScopeObject();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                if (disposing)
                {
                    _semaphore.Dispose();
                }
                disposedValue = true;
            }
        }

        protected override void OnScopeEnded(object? sender, EventArgs e)
        {
            _semaphore.Release();
        }

        ~ScopedSemaphoreSlim()
        {
            if (!base.disposedValue)
            {
                base.Dispose(true);
            }
        }
    }
}
