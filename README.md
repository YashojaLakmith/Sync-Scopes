# Sync-Scopes

Sync-Scopes allows you to encapsulate common synchronization primitives in .NET to create and use synchronization scopes that follows the dispose pattern instead of using try-finally blocks.

## How to use
```csharp
class Example
{
	private readonly ScopedLock _locker = new ScopedLock();

	public void DoSomething()
	{
		using(var scope = _locker.CreateScope())
		{
			// Do something under synchronization.
		}

		// Do something outside synchronization.
	}
}
```

## Supported Synchronization Primitives
Sync-Scopes currently encapsulates the following synchronization primitives in .NET.
- [Lock](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock) through the `ScopedLock` class.
- [Mutex](https://learn.microsoft.com/en-us/dotnet/api/system.threading.mutex) through the `ScopedMutex` class.
- [Semaphore](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphore) through the `ScopedSemaphore` class.
- [SemaphoreSlim](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim) through the `ScopedSemaphoreSlim` class.

## License
Sync-Scopes is licensed under the [MIT License](./LICENSE.txt)