using Application.Abstractions;

namespace Infrastructure.Logging;

public sealed class ConsoleAppLogger : IAppLogger
{
    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {DateTimeOffset.Now:O} - {message}");
    }
}
