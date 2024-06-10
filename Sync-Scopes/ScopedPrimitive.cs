using System;

namespace Sync_Scopes
{
    public abstract class ScopedPrimitive : IDisposable
    {
        protected bool disposedValue;

        public abstract SynchronizationScope CreateScope();

        public abstract SynchronizationScope CreateScope(int milisecondTimeout);

        public abstract SynchronizationScope CreateScope(TimeSpan timeout);

        public abstract SynchronizationScope CreateScope(int milisecondTimeout, bool exitContext);

        public abstract SynchronizationScope CreateScope(TimeSpan timeout, bool exitContext);

        protected abstract void OnScopeRelease(object? sender, EventArgs e);

        protected SynchronizationScope CreateNewScopeObject()
        {
            var scope = new SynchronizationScope();
            scope.ScopeEnded += OnScopeRelease;
            return scope;
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
    }
}