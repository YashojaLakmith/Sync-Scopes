using FluentAssertions;

namespace Sync_Scopes.UnitTests;

[TestFixture]
public class SynchronizationScopeUnitTests
{
    [Test]
    public void SynchronizationScope_OnDispose_MustRaiseScopeEnded()
    {
        bool isEventRaised = false;
        var scope = new SynchronizationScope();
        scope.ScopeEnded += delegate (object? sender, EventArgs e)
        {
            isEventRaised |= true;
        };

        scope.Dispose();

        isEventRaised.Should().BeTrue();
    }

    [Test]
    public void SynchronizationScope_NewObject_ReleasableOnlyOnce()
    {
        var invokes = new List<object?>();
        var scope = new SynchronizationScope();
        scope.ScopeEnded += delegate (object? sender, EventArgs e)
        {
            invokes.Add(sender);
        };

        scope.Dispose();
        scope.Dispose();

        invokes.Count.Should().Be(1);
    }

    [Test]
    public void SynchronizationScope_ScopeEndedEvent_MustBeHandledByScopeOwningThread()
    {
        var scope1 = new SynchronizationScope();
        var scope2 = new SynchronizationScope();
        int tid1 = 0;
        int tid2 = 0;

        scope1.ScopeEnded += delegate (object? sender, EventArgs e)
        {
            Thread.Sleep(100);
            tid1 = Environment.CurrentManagedThreadId;
        };

        scope2.ScopeEnded += delegate (object? sender, EventArgs e)
        {
            tid2 = Environment.CurrentManagedThreadId;
        };

        var t1 = new Thread(scope1.Dispose);
        var t2 = new Thread(scope2.Dispose);

        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();

        tid1.Should().NotBe(tid2);
    }
}
