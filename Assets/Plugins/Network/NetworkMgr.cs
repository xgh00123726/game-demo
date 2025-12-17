using GameBase.Tools;
using System.Net;
using UnityEngine;

namespace GameBase.Network
{
    public class NetworkMgr : SingletonInstance<NetworkMgr>
    {
        private int _frameSinceLastSendHeartbeat = 0;

        public NetworkMgr()
        {
            NetworkProtoMgr.Init();
            ServerHelper.ConnectToServer();
            ServerHelper.ListenMessage();
        }

        protected override bool IsFixedUpdate => true;

        protected override void Update()
        {
            NetworkProtoMgr.Update();
        }

        protected override void FixedUpdate()
        {
            _frameSinceLastSendHeartbeat++;
            if (_frameSinceLastSendHeartbeat > ServerHelper.SEND_HEARTBEAT_INTERVAL)
            {
                ServerHelper.SendHeartBeat();
                _frameSinceLastSendHeartbeat = 0;
            }

            ServerHelper.StayConnected(Time.time);
        }

        public void Dispose()
        {
            ServerHelper.Dispose();
        }
    }
}
