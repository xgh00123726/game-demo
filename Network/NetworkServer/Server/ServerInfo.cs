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
        ServerLogger.Log($"服务器已启动，监听地址：{IPAddress}:{Port}");
        ServerLogger.Log("等待客户端连接...");
        while (true)
        {
            try
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                CM.AddClient(client);
            }
            catch (Exception ex)
            {
                ServerLogger.Log($"接收客户端连接失败：{ex.Message}");
                break;
            }
        }
    }
}