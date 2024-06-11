using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedLock : ScopedPrimitive
    {
        private readonly object _lock = new object();
        private bool _lockTaken = false;

        public override SynchronizationScope CreateScope()
        {
            base.ThrowIfDisposed();
            Monitor.Enter(_lock, ref _lockTaken);
            return base.CreateNewScopeObject();
        }

        protected override void Dispose(bool disposing)
        {
            if (!base.disposedValue)
            {
                if (disposing)
                {
                    if (_lockTaken)
                    {
                        Monitor.Exit(_lock);
                    }
                }
                base.disposedValue = true;
            }
        }

        protected override void OnScopeEnded(object? sender, EventArgs e)
        {
            if (_lockTaken)
            {
                Monitor.Exit(_lock);
                _lockTaken = false;
            }
        }

        ~ScopedLock()
        {
            if (!base.disposedValue)
            {
                base.Dispose(true);
            }
        }
    }
}