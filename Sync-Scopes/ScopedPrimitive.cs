using System;

namespace Sync_Scopes
{
    public abstract class ScopedPrimitive : ILockReleaser, IDisposable
    {
        protected bool disposedValue;

        public abstract SynchronizationScope CreateScope();

        public abstract SynchronizationScope CreateScope(int milisecondTimeout);

        public abstract SynchronizationScope CreateScope(TimeSpan timeout);

        public abstract SynchronizationScope CreateScope(int milisecondTimeout, bool exitContext);

        public abstract SynchronizationScope CreateScope(TimeSpan timeout, bool exitContext);

        protected SynchronizationScope CreateNewScopeObject()
        {
            return new SynchronizationScope(this);
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

        public abstract void ReleaseLock();
    }
}