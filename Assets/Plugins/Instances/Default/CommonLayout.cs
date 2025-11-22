using GameBase.UI;
using UnityEngine;

namespace Instance
{
    public enum AlignType
    {
        Left = 1 << 0, 
        Right = 1 << 1, 
        HCenter = 1 << 2,

        Top = 1 << 16,
        Bottom = 1 << 17,
        VCenter = 1 << 18,
    }
    public class CommonLayout : ILayout
    {
        public float XInterval { get; set; } = 115;
        public float YInterval { get; set; } = 115;
        public float Width { get; set; } = 1200;
        public float Height { get; set; } = 600;
        public AlignType Align { get; set; } = AlignType.Left | AlignType.Bottom;

        public Vector3 GetItemLocalPosition(int index)
        {
            var itemPerLine = (int)Mathf.Floor(Width / (XInterval));
            var x = index % itemPerLine;
            var y = index / itemPerLine;

            float vx = 0f;
            if ((Align & AlignType.Left) != 0)
            {
                vx = (XInterval) * x;
            }
            else if ((Align & AlignType.HCenter) != 0)
            {
                vx = (XInterval) * x;
                float remainWidth = itemPerLine * (XInterval);
                vx -= remainWidth / 2;
            }

            float vy = 0f;
            if ((Align & AlignType.Top) != 0)
            {
                vy = Height - YInterval * y;
            }
            else if ((Align & AlignType.Bottom) != 0)
            {
                vy = YInterval * y;
            }


            return new Vector3(vx, vy, 0);
        }
    }
}
