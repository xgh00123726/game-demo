using NetworkServer.Input;

namespace NetworkServer;

internal class ConsoleLogger : ILogger
{
    public void Log(object message)
    {
        Console.WriteLine($"[INFO][{DateTime.Now}][{DateTime.Now.Microsecond, 4}]{message}");
    }
}
