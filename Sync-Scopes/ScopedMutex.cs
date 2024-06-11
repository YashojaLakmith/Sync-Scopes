using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedMutex : ScopedPrimitive
    {
        private readonly Mutex _mutex;

        public ScopedMutex()
        {
            _mutex = new Mutex();
        }

        public ScopedMutex(Mutex mutex)
        {
            _mutex = mutex;
        }

        public override SynchronizationScope CreateScope()
        {
            base.ThrowIfDisposed();
            _mutex.WaitOne();
            return base.CreateNewScopeObject();
        }

        protected override void Dispose(bool disposing)
        {
            if (base.disposedValue)
            {
                if (disposing)
                {
                    _mutex.Dispose();
                }
                base.disposedValue = true;
            }
        }

        protected override void OnScopeEnded(object? sender, EventArgs e)
        {
            _mutex.ReleaseMutex();
        }

        ~ScopedMutex()
        {
            if (!base.disposedValue)
            {
                base.Dispose(true);
            }
        }
    }
}