using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Network
{
    public abstract class NetworkProtoParam
    {
        public abstract void Receive(IMessage msg);
        public abstract void Update();
    }
    public class NetworkProtoParam<T> : NetworkProtoParam
        where T : class, IMessage
    {
#nullable enable
        public Action<T>? OnRecv { get; set; }
#nullable disable
        private Queue<T> _msgQueue = new();
        public void RegisterRecvEvent(Action<T> action)
        {
            OnRecv += action;
        }
        public void UnRegisterRecvEvent(Action<T> action)
        {
            OnRecv -= action;
        }
        public override void Update()
        {
            while (_msgQueue.TryDequeue(out var msg))
            {
                OnRecv?.Invoke(msg);
            }
        }
        public override void Receive(IMessage msg)
        {
            if (msg is T msgT)
            {
                _msgQueue.Enqueue(msgT);
            }
        }
    }
}
