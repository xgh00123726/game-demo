using Google.Protobuf;
using System;

namespace GameBase.Network
{
    public abstract class NetworkProtoParam
    {
        public abstract void Receive(IMessage msg);
    }
    public class NetworkProtoParam<T> : NetworkProtoParam
        where T : class, IMessage
    {
#nullable enable
        public Action<T>? OnRecv { get; set; }
#nullable disable
        public void RegisterRecvEvent(Action<T> action)
        {
            OnRecv += action;
        }
        public void UnRegisterRecvEvent(Action<T> action)
        {
            OnRecv -= action;
        }
        public override void Receive(IMessage msg)
        {
            if (msg is T msgT)
            {
                OnRecv?.Invoke(msgT);
            }
        }
    }
}
