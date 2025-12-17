using GameBase.Tools;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GameBase.Network
{
    public class ServerHelper
    {
        public const int SEND_HEARTBEAT_INTERVAL = 120; // frame

        private static bool _isConnected = false;
        private static bool _isConnecting = false;
        private static float _lastTryConnectTime = 0f;
        private static int _tryConnectTimes = 0;
        private static TcpClient _tcpClient;
        private static Thread _listenMessageThread;

        private static IPAddress _serverIP;
        private static int _serverPort;

        private static float RetryInterval
        {
            get
            {
                return 1f * (1 << _tryConnectTimes);
            }
        }

        static ServerHelper()
        {
            _serverIP = IPAddress.Parse("127.0.0.1");
            _serverPort = 8888;
        }

        private static void ListeningMessage()
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                if (!_isConnected)
                {
                    continue;
                }
                try
                {
                    using var stream = _tcpClient.GetStream();
                    while (true)
                    {
                        try
                        {
                            if (!_isConnected)
                            {
                                continue;
                            }

                            var len = stream.Read(buffer, 0, buffer.Length);
                            if (len == 0)
                            {
                                XLogger.Instance.InSubThread().Log("Close connect to server");
                                _tcpClient.Close();
                            }

                            NetworkProtoMgr.ReceiveBytes(buffer, 0, len);
                        }
                        catch (Exception e)
                        {
                            XLogger.Instance.InSubThread()
                                .Log($"Connect exception:{e}, thread has sleep");
                        }
                    }
                }
                catch (Exception e)
                {
                    XLogger.Instance.InSubThread()
                        .Log($"Connect exception:{e}, thread has sleep");
                }
            }
        }

        internal static void StayConnected(float time)
        {
            if (!_isConnected && !_isConnecting && time > _lastTryConnectTime + RetryInterval)
            {
                _tryConnectTimes++;
                _lastTryConnectTime = time;
                _isConnecting = true;
                _tcpClient.ConnectAsync(_serverIP, _serverPort).ContinueWith(t =>
                {
                    _isConnected = _tcpClient.Connected;
                    _isConnecting = false;
                    if (_isConnected)
                    {
                        _tryConnectTimes = 0;
                        XLogger.Instance.InSubThread()
                            .Log("Connect ok");
                    }
                    else
                    {
                        XLogger.Instance.InSubThread()
                            .Log($"Connect fail, next connect in {RetryInterval} second");
                    }
                });
            }
        }

        public static void ManualConnect()
        {
            _tryConnectTimes = 0;
        }

        public static void ConnectToServer()
        {
            _tcpClient = new TcpClient();
        }

        public static void ListenMessage()
        {
            _listenMessageThread ??= new Thread(ListeningMessage)
            {
                IsBackground = true
            };
            _listenMessageThread.Start();
        }

        public static void DisConnect()
        {
            _isConnected = false;
            _tcpClient.Close();
            XLogger.Instance.Level(XLogger.LogLevel.Warning).Log("Client disconnected");
        }

        public static void Dispose()
        {
            _tcpClient.Dispose();
            _listenMessageThread.Abort();
        }

        public static void StopListen()
        {
            _listenMessageThread.Abort();
        }

        public static void SendHeartBeat()
        {
            if (!_isConnected)
            {
                return;
            }

            try
            {
                _tcpClient.Client.Send(new byte[1]
                {
                    0xFE,
                });
            }
            catch (ObjectDisposedException)
            {
                XLogger.Instance.Log("Unable to connect server");
                _isConnected = false;
            }
            catch (SocketException)
            {
                XLogger.Instance.Log("Unable to connect server");
                _isConnected = false;
            }
        }
    }
}
