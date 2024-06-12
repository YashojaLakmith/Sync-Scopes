using System;

namespace Sync_Scopes
{
    /// <summary>
    /// Abstract base class which all the synchronization primitive encapsulations must derive from.
    /// </summary>
    public abstract class ScopedPrimitive : IDisposable
    {
        protected bool disposedValue;

        /// <summary>
        /// Blocks the calling thread untill a primitive was acquired and returns a <see cref="SynchronizationScope"/>.
        /// </summary>
        /// <returns>A <see cref="SynchronizationScope"/> which represents an acquired primitive.</returns>
        public abstract SynchronizationScope CreateScope();

        /// <summary>
        /// Creates a new <see cref="SynchronizationScope"/> for the calling thread.
        /// </summary>
        /// <returns>Created <see cref="SynchronizationScope"/></returns>
        protected SynchronizationScope CreateNewScopeObject()
        {
            var scope = new SynchronizationScope();
            scope.ScopeEnded += OnScopeEnded;
            return scope;
        }

        /// <summary>
        /// An event handler which responds when the <see cref="SynchronizationScope"/> of the calling thread attached to this synchronization primitive has been disposed.
        /// </summary>
        protected abstract void OnScopeEnded(object? sender, EventArgs e);

        private protected void ThrowIfDisposed()
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
    }
}