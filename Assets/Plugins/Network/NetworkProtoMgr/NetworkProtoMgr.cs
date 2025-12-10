using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace GameBase.Network
{
    public static partial class NetworkProtoMgr
    {
        private const int ID_LEN = 2;
        private static List<NetworkProtoParam> _params = new();
        private static List<MessageParser> _parsers = new();


        public static void ReceiveBytes(byte[] buffer, int offset, int len)
        {
            if (len <= ID_LEN)
            {
                return;
            }
            Span<byte> span = buffer.AsSpan(offset, ID_LEN);
            int index = 0;
            foreach (var b in span)
            {
                index += b;
                index <<= 8;
            }
            index >>= 8;
            if (index >= _params.Count)
            {
                return;
            }
            _params[index].Receive(_parsers[index].ParseFrom(buffer, offset + ID_LEN, len - ID_LEN));
        }

        public static void Init()
        {
            WarpperInit();
        }
    }
}
