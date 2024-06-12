using System;

namespace Sync_Scopes
{
    /// <summary>
    /// Represents an acquired synchronization scope by the calling thread.
    /// </summary>
    public struct SynchronizationScope : IDisposable
    {
        internal event EventHandler? ScopeEnded;
        private bool _disposed;

        /// <summary>
        /// Releases the scope owned by the calling thread.
        /// </summary>
        public void Release()
        {
            OnScopeEnded();
            ScopeEnded = null;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Release();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private readonly void OnScopeEnded()
        {
            ScopeEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
