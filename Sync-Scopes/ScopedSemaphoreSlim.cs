using System;
using System.Threading;

namespace Sync_Scopes
{
    /// <summary>
    /// Encapsulates a <see cref="SemaphoreSlim"/>.
    /// </summary>
    public class ScopedSemaphoreSlim : ScopedPrimitive
    {
        private readonly SemaphoreSlim _semaphore;

        /// <summary>
        /// Creates an instance using a default <see cref="SemaphoreSlim"/> specifying the number of initial entries and the number of maximum concurrent entries.
        /// </summary>
        /// <param name="initialCount">Initial number of entries.</param>
        /// <param name="maxCount">Maximum number of concurrent entries.</param>
        public ScopedSemaphoreSlim(int initialCount, int maxCount)
        {
            _semaphore = new SemaphoreSlim(initialCount, maxCount);
        }

        /// <summary>
        /// Creates an instance using the provided <see cref="SemaphoreSlim"/>.
        /// </summary>
        public ScopedSemaphoreSlim(SemaphoreSlim semaphoreSlim)
        {
            _semaphore = semaphoreSlim;
        }

        /// <summary>
        /// Gets the encapsulated <see cref="SemaphoreSlim"/> by this instance.
        /// </summary>
        /// <returns>The <see cref="SemaphoreSlim"/> object used by this instance.</returns>
        public SemaphoreSlim GetEncapsulatedSemaphoreSlim()
        {
            base.ThrowIfDisposed();
            return _semaphore;
        }

        public override SynchronizationScope CreateScope()
        {
            base.ThrowIfDisposed();
            _semaphore.Wait();
            return CreateNewScopeObject();
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

        ~ScopedSemaphoreSlim()
        {
            if (!base.disposedValue)
            {
                base.Dispose(true);
            }
        }
    }
}
