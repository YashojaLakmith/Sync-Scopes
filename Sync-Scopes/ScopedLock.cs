using System;
using System.Threading;

namespace Sync_Scopes
{
    public class ScopedLock : ScopedPrimitive
    {
        private readonly object _lock = new object();
        private bool _lockTaken = false;

        public override SynchronizationScope CreateScope()
        {
            Monitor.Enter(_lock, ref _lockTaken);
            return CreateNewScopeObject();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout)
        {
            throw new NotImplementedException();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public override SynchronizationScope CreateScope(int milisecondTimeout, bool exitContext)
        {
            throw new NotImplementedException();
        }

        public override SynchronizationScope CreateScope(TimeSpan timeout, bool exitContext)
        {
            throw new NotImplementedException();
        }

        protected override void OnScopeRelease(object? sender, EventArgs e)
        {
            if (_lockTaken)
            {
                Monitor.Exit(_lock);
            }
        }       
    }
}