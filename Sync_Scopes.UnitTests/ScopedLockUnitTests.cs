using FluentAssertions;

namespace Sync_Scopes.UnitTests;

[TestFixture]
public class ScopedLockUnitTests
{
    [Test]
    public void CreateScope_ShouldReturnANonDisposedScope()
    {
        using var locker = new ScopedLock();
        bool isEventFired = false;
        var scope = locker.CreateScope();
        scope.ScopeEnded += delegate (object? sender, EventArgs e)
        {
            isEventFired |= true;
        };

        scope.Dispose();

        isEventFired.Should().BeTrue();
    }
}
