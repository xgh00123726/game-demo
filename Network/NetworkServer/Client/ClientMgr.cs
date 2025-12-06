using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetworkServer.Client;

internal class ClientMgr
{
    private Dictionary<string, ClientInfo> _clients = new();
    private readonly object _clientLock = new object();
    private Stopwatch _stopwatch;
    public required ILogger Logger {  get; set; }

    public ClientMgr()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    /// <summary>
    /// 添加客户端
    /// </summary>
    /// <param name="client"></param>
    public void AddClient(TcpClient client)
    {
        var id = GenerateClientId(client);
        var thread = new Thread(() =>
        {
            var clientInfo = _clients[id];
            if (clientInfo == null)
            {
                Logger.Log("非法时序调用");
                return;
            }
            using var stream = client.GetStream();
            byte[] buffer = new byte[1024];
            while (client.Connected)
            {
                var len = stream.Read(buffer, 0, buffer.Length);
                if (len == 0)
                {
                    Logger.Log("客户端关闭连接");
                    client.Close();
                    break;
                }

                clientInfo.OnReceiveMsg?.Invoke(clientInfo, buffer, len);
            }
        });

        lock(_clientLock)
        {
            _clients[id] = new ClientInfo()
            {
                Client = client,
                HeartBeatInterval = 1000,
                DisconnectTimeThreshold = 3000,
                DisconnectThreshold = 3,
                ReceiveThread = thread,
                OnReceiveMsg = OnReceiveMsg,
                LastHeartBeatTime = _stopwatch.ElapsedMilliseconds,
            };
        }
        thread.Start();
        Logger.Log($"客户端:{id} 已连接");
    }

    public void LogInfo()
    {
        Logger.Log($"{DateTime.Now} Active client num: {_clients.Count}");
        foreach (var kvp in _clients)
        {
            Logger.Log($"Client:{kvp.Key}, connect status:{kvp.Value.IsConnected}");
        }
    }

    /// <summary>
    /// 根据id获取客户端
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ClientInfo? GetClient(string id)
    {
        ClientInfo? info = null;
        lock (_clientLock)
        {
            if (_clients.TryGetValue(id, out ClientInfo? value))
            {
                info = value;
            }
        }
        return info;
    }

    /// <summary>
    /// 维持心跳，处理不活却的客户端
    /// </summary>
    public void KeepClients()
    {
        while (true)
        {
            var time = _stopwatch.ElapsedMilliseconds;
            List<string> inactives = [];
            foreach (var kvp in _clients)
            {
                var id = kvp.Key;
                var client = kvp.Value;
                if (time - client.LastHeartBeatTime >= client.DisconnectTimeThreshold)
                {
                    client.IsConnected = false;
                    Logger?.Log($"发送心跳失败超过 {client.DisconnectTimeThreshold} ms，连接已断开");
                    client.Client.Close();
                    inactives.Add(id);
                }
            }

            lock (_clientLock)
            {
                foreach (var id in inactives)
                {
                    _clients.Remove(id);
                }
            }
            inactives.Clear();
        }
    }

    public bool Boardcast(object message)
    {
        var result = false;
        foreach (var kvp in _clients)
        {
            var client = kvp.Value.Client;
            using var stream = client.GetStream();
            if (message == null)
            {
                continue;
            }
            var mStr = message.ToString();
            if (mStr == null)
            {
                continue;
            }
            stream.Write(Encoding.UTF8.GetBytes(mStr));
            stream.Flush();
            result = true;
        }

        return result;
    }

    private void OnReceiveMsg(ClientInfo clientInfo, byte[] buffer, int len)
    {
        var time = DateTime.Now.Microsecond;
        if (len == 1 && buffer[0] == 0xFE)
        {
            clientInfo.LastHeartBeatTime = time;
        }
        Logger.Log($"接收到长度为:{len} 的数据包");
    }


    private static string GenerateClientId(TcpClient client)
    {
        var remoteEndPoint = (IPEndPoint?)client.Client.RemoteEndPoint;
        return $"{remoteEndPoint?.Address}:{remoteEndPoint?.Port}";
    }
}
