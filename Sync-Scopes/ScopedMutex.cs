using System;
using System.Threading;

namespace Sync_Scopes
{
    /// <summary>
    /// Encapsulates a <see cref="Mutex"/>
    /// </summary>
    public class ScopedMutex : ScopedPrimitive
    {
        private readonly Mutex _mutex;

        /// <summary>
        /// Creates an instance using an default <see cref="Mutex"/> instance.
        /// </summary>
        public ScopedMutex()
        {
            _mutex = new Mutex();
        }

        /// <summary>
        /// Creates an instance using a provided <see cref="Mutex"/>.
        /// </summary>
        public ScopedMutex(Mutex mutex)
        {
            _mutex = mutex;
        }

        /// <summary>
        /// Gets the encapsulated <see cref="Mutex"/> object.
        /// </summary>
        /// <returns>The <see cref="Mutex"/> object used by this instance.</returns>
        public Mutex GetEncapsulatedMutex()
        {
            base.ThrowIfDisposed();
            return _mutex;
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