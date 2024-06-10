using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedMutex : ScopedPrimitive
    {
        private readonly Mutex _mutex;

        public ScopedMutex()
        {
            _mutex = new Mutex();
        }

        public ScopedMutex(Mutex mutex)
        {
            _mutex = mutex;
        }

        public override SynchronizationScope CreateScope()
        {
            _mutex.WaitOne();
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout)
        {
            _mutex.WaitOne(milisecondTimeout);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout)
        {
            _mutex.WaitOne(timeout);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout, bool exitContext)
        {
            _mutex.WaitOne(milisecondTimeout, exitContext);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout, bool exitContext)
        {
            _mutex.WaitOne(timeout, exitContext);
            return CreateNewScopeObject();
        }

        public override void ReleaseLock()
        {
            _mutex.ReleaseMutex();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                if (disposing)
                {
                    _mutex.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}