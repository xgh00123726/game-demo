using NetworkServer.Client;
using System.Net;
using System.Net.Sockets;

namespace NetworkServer.Server;

internal class ServerInfo
{
    public required IPAddress IPAddress { get; set; }
    public required int Port { get; set; }
    public required ILogger ServerLogger { get; set; }
    public required ClientMgr CM { get; set; }

    public void ListenClient()
    {
        var tcpListener = new TcpListener(IPAddress, Port);
        tcpListener.Start();
        ServerLogger.Log($"Server start, Listening: {IPAddress}:{Port}");
        ServerLogger.Log("Waiting connect...");
        while (true)
        {
            try
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                CM.AddClient(client);
            }
            catch (Exception ex)
            {
                ServerLogger.Log($"Recv connect fail: {ex.Message}");
                break;
            }
        }
    }
}