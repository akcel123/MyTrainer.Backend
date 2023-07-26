using System;
using Microsoft.Extensions.Logging;
using MyTrainer.API.Controllers;

namespace MyTrainer.UnitTests.Infrastructure;

public class MockLogger: ILogger<TrainingsController>, IDisposable
{

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        => this;

    public bool IsEnabled(LogLevel logLevel)
        => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine(state);
    }

    public void Dispose()
    { }
}

