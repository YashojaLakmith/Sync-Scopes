using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedSemaphore : ScopedPrimitive
    {
        private readonly Semaphore _semaphore;

        public ScopedSemaphore(int initialCount, int maxCount)
        {
            _semaphore = new Semaphore(initialCount, maxCount);
        }

        public ScopedSemaphore(Semaphore semaphore)
        {
            _semaphore = semaphore;
        }

        public override SynchronizationScope CreateScope()
        {
            base.ThrowIfDisposed();
            _semaphore.WaitOne();
            return base.CreateNewScopeObject();
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

        ~ScopedSemaphore()
        {
            if (!base.disposedValue)
            {
                base.Dispose(true);
            }
        }
    }
}
