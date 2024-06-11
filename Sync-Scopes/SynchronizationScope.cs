using System;

namespace Sync_Scopes
{
    public struct SynchronizationScope : IDisposable
    {
        internal event EventHandler? ScopeEnded;
        private bool _disposed;

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            OnScopeEnded();
            ScopeEnded = null;
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private readonly void OnScopeEnded()
        {
            ScopeEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
