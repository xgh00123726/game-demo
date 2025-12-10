using Google.Protobuf;
using System;
namespace GameBase.Network
{
    public partial class NetworkProtoMgr
    {
        private static byte[] MergeBytes(params byte[][] arrays)
        {
            int totalLength = 0;
            foreach (var array in arrays)
            {
                totalLength += array.Length;
            }

            byte[] result = new byte[totalLength];

            Span<byte> resultSpan = result.AsSpan();
            int offset = 0;

            foreach (var array in arrays)
            {
                array.AsSpan().CopyTo(resultSpan.Slice(offset));
                offset += array.Length;
            }

            return result;
        }

        private static byte[] ToBytes(int val, int len)
        {
            var ret = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ret[i] = (byte)(val & (0xFF << (8 * i)));
            }
            return ret;
        }

        public static byte[] ToFrame(this Google.Protobuf.IMessage message)
        {
            var type = message.GetType();
            if (_messageTypesMap.TryGetValue(type, out var id))
            {
                return MergeBytes(ToBytes(id, ID_LEN), message.ToByteArray());
            }

            return new byte[1] { 0xFF };
        }
    }
}
