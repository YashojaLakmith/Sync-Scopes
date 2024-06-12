using System;
using System.Threading;

namespace Sync_Scopes
{
    /// <summary>
    /// Encapsulates a <see cref="Semaphore"/>.
    /// </summary>
    public class ScopedSemaphore : ScopedPrimitive
    {
        private readonly Semaphore _semaphore;

        /// <summary>
        /// Creates an instance using a default <see cref="Semaphore"/> with specified initial number of entries and specified number of concurrent entries.
        /// </summary>
        /// <param name="initialCount">Initial number of entries.</param>
        /// <param name="maxCount">Maximum number of concurrent entries.</param>
        public ScopedSemaphore(int initialCount, int maxCount)
        {
            _semaphore = new Semaphore(initialCount, maxCount);
        }

        /// <summary>
        /// Creates an instance using the provided <see cref="Semaphore"/>.
        /// </summary>
        public ScopedSemaphore(Semaphore semaphore)
        {
            _semaphore = semaphore;
        }

        /// <summary>
        /// Gets the encapsulated <see cref="Semaphore"/> by this instance.
        /// </summary>
        /// <returns>The <see cref="Semaphore"/> object used by this instance.</returns>
        public Semaphore GetEncapsulatedSemaphore()
        {
            base.ThrowIfDisposed();
            return _semaphore;
        }

        public override SynchronizationScope CreateScope()
        {
            base.ThrowIfDisposed();
            _semaphore.WaitOne();
            return base.CreateNewScopeObject();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                if (disposing)
                {
                    _semaphore.Dispose();
                }
                disposedValue = true;
            }
        }

        protected override void OnScopeEnded(object? sender, EventArgs e)
        {
            _semaphore.Release();
        }

        ~ScopedSemaphore()
        {
            if (!base.disposedValue)
            {
                base.Dispose(true);
            }
        }
    }
}
