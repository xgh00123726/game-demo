using NetworkServer.Input;

namespace NetworkServer;

internal class ConsoleLogger : ILogger
{
    public void Log(object message)
    {
        Console.WriteLine($"[INFO]{message}");
    }
}
