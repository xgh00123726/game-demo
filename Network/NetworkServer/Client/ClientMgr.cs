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
    public bool IsLogHeartbeat { get; set; }

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
        var cts = new CancellationTokenSource();
        var id = GenerateClientId(client);
        var thread = new Thread(async () =>
        {
            try
            {
                var clientInfo = _clients[id];
                if (clientInfo == null)
                {
                    Logger.Log("Illegal call");
                    return;
                }
                using var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                while (clientInfo.IsConnected)
                {
                    var len = await stream.ReadAsync(buffer, cts.Token);
                    if (len == 0)
                    {
                        Logger.Log("Client close connect");
                        client.Close();
                        break;
                    }

                    clientInfo.OnReceiveMsg?.Invoke(clientInfo, buffer, len);
                }
            }
            catch(OperationCanceledException)
            {
                Logger.Log($"Client {id} disconnect due to cancel");
            }
            catch(IOException)
            {
                Logger.Log($"Client {id} disconnect due to remote");
            }
            finally
            {
                lock(_clientLock)
                {
                    _clients.Remove(id);
                }
                cts.Dispose();
            }
        });

        lock(_clientLock)
        {
            _clients[id] = new ClientInfo()
            {
                Client = client,
                DisconnectTimeThreshold = 30000,
                ReceiveThread = thread,
                CTS = cts,
                OnReceiveMsg = OnReceiveMsg,
                LastHeartBeatTime = _stopwatch.ElapsedMilliseconds,
                IsConnected = true,
            };
        }
        thread.Start();
        Logger.Log($"Client: {id} connected");
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
                    Logger.Log($"Disconnect due to client: {id} HB fail over {client.DisconnectTimeThreshold} ms");
                    client.CTS.Cancel();
                    client.ReceiveThread.Join();
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

    public bool Boardcast(byte[] message)
    {
        if (message == null)
        {
            Logger.Log("Warning, Try to boardcast null messae");
            return false;
        }

        var result = false;
        foreach (var kvp in _clients)
        {
            var client = kvp.Value.Client;
            var len = client.Client.Send(message);
            if (len > 0)
            {
                result = true;
            }
        }

        return result;
    }

    private void OnReceiveMsg(ClientInfo clientInfo, byte[] buffer, int len)
    {
        if (len == 1 && buffer[0] == 0xFE)
        {
            clientInfo.LastHeartBeatTime = _stopwatch.ElapsedMilliseconds;
            if (IsLogHeartbeat)
            {
                Logger.Log($"Recv HB");
            }
        }
    }


    private static string GenerateClientId(TcpClient client)
    {
        var remoteEndPoint = (IPEndPoint?)client.Client.RemoteEndPoint;
        return $"{remoteEndPoint?.Address}:{remoteEndPoint?.Port}";
    }
}
