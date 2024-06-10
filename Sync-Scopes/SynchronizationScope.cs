using System;

namespace Sync_Scopes
{
    public struct SynchronizationScope : IDisposable
    {
        private ILockReleaser? _lockReleaser;
        private bool _disposed;

        internal SynchronizationScope(ILockReleaser lockReleaser)
        {
            _lockReleaser = lockReleaser;
            _disposed = false;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                return;
            }

            _lockReleaser?.ReleaseLock();
            _disposed = true;
            _lockReleaser = null;
            GC.SuppressFinalize(this);
        }
    }
}
