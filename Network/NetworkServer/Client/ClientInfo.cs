using System.Net.Sockets;

namespace NetworkServer.Client;

internal class ClientInfo
{
    public required TcpClient Client {  get; set; }
    public required int DisconnectTimeThreshold { get; set; }
    public required Thread ReceiveThread { get; set; }
    public required CancellationTokenSource CTS { get; set; }
    public long LastHeartBeatTime { get; set; }
    public bool IsConnected { get; set; }
    public Action<ClientInfo, byte[], int>? OnReceiveMsg { get; set; }
}
