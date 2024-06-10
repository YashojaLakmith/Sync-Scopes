using System.Threading;
using System;

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
            _semaphore.WaitOne();
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout)
        {
            _semaphore.WaitOne(milisecondTimeout);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout)
        {
            _semaphore.WaitOne(timeout);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout, bool exitContext)
        {
            _semaphore.WaitOne(milisecondTimeout, exitContext);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout, bool exitContext)
        {
            _semaphore.WaitOne(timeout, exitContext);
            return CreateNewScopeObject();
        }

        protected override void OnScopeRelease(object? sender, EventArgs e)
        {
            _semaphore.Release();
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
    }
}
