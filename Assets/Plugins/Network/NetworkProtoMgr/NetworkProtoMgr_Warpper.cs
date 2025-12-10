using System;
using System.Collections.Generic;

namespace GameBase.Network
{
    public static partial class NetworkProtoMgr
    {
        private static Dictionary<Type, int> _messageTypesMap = new()
        {
            { typeof(Notify), 0},
        };

        public static NetworkProtoParam<Notify> Notify { get; } = new();


        private static void WarpperInit()
        {
            _params.Add(NetworkProtoMgr.Notify);
            
            _parsers.Add(GameBase.Network.Notify.Parser);
        }
    }
}
