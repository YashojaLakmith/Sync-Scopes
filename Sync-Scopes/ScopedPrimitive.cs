using System;

namespace Sync_Scopes
{
    public abstract class ScopedPrimitive : IDisposable
    {
        protected bool disposedValue;

        public abstract SynchronizationScope CreateScope();

        protected SynchronizationScope CreateNewScopeObject()
        {
            var scope = new SynchronizationScope();
            scope.ScopeEnded += OnScopeEnded;
            return scope;
        }

        protected void ThrowIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected abstract void OnScopeEnded(object? sender, EventArgs e);
    }
}