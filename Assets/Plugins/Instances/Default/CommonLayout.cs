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
        public float xInterval = 115;
        public float yInterval = 115;
        public float width = 1200;
        public float height = 600;
        public AlignType align = AlignType.Left | AlignType.Bottom;

        public Vector3 GetItemLocalPosition(int index)
        {
            var itemPerLine = (int)Mathf.Floor(width / (xInterval));
            var x = index % itemPerLine;
            var y = index / itemPerLine;

            float vx = 0f;
            if ((align & AlignType.Left) != 0)
            {
                vx = (xInterval) * x;
            }
            else if ((align & AlignType.HCenter) != 0)
            {
                vx = (xInterval) * x;
                float remainWidth = itemPerLine * (xInterval);
                vx -= remainWidth / 2;
            }

            float vy = 0f;
            if ((align & AlignType.Top) != 0)
            {
                vy = height - yInterval * y;
            }
            else if ((align & AlignType.Bottom) != 0)
            {
                vy = yInterval * y;
            }


            return new Vector3(vx, vy, 0);
        }
    }
}
