using System;

namespace Sync_Scopes
{
    public struct SynchronizationScope : IDisposable
    {
        internal event EventHandler? ScopeEnded;

        public void Dispose()
        {
            OnScopeEnd();
            ScopeEnded = null;
            GC.SuppressFinalize(this);
        }

        private readonly void OnScopeEnd()
        {
            ScopeEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
