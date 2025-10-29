using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Line : IShape2D
        {
            public float width;
            public Vector2 begin;
            public Vector2 end;
            public float pivot;

            public Line(Vector2 begin, Vector2 end)
            {
                this.begin = begin;
                this.end = end;
                width = 0;
                pivot = 0.5f;
            }

            public Line(Vector2 begin, Vector2 dir, float length)
            {
                this.begin = begin;
                this.end = begin + dir.normalized * length;
                width = 0;
                pivot = 0.5f;
            }

            public Vector2 Dir
            {
                get => end - begin;
                set
                {
                    var len = Length;
                    end = value.normalized * len + begin;
                }
            }
            public float Length
            {
                get => Dir.magnitude;
                set
                {
                    end = begin + Dir.normalized * value;
                }
            }
            public Vector2 LeftBottom => begin - GMath.VerticalVector2(Dir).normalized * width / 2;
            public Vector2 LeftTop => end - GMath.VerticalVector2(Dir).normalized * width / 2;
            public Vector2 RightBottom => begin + GMath.VerticalVector2(Dir).normalized * width / 2;
            public Vector2 RightTop => end + GMath.VerticalVector2(Dir).normalized * width / 2;
            Vector2 IShape2D.Center
            {
                get => begin + pivot * Dir;
                set
                {
                    var dir = Dir;
                    begin = value - dir * pivot;
                    end = value + dir * (1 - pivot);
                }
            }

            float IShape2D.Size
            {
                get => width;
                set
                {
                    Length = Length * value / width;
                    width = value;
                }
            }

            bool IShape2D.Contains(float x, float y)
            {
                var dir1 = end - begin;
                var dir2 = new Vector2(x, y) - begin;
                float angle1 = Vector2.Angle(dir1, dir2);
                if (angle1 > 90f)
                {
                    return false;
                }
                var dir3 = new Vector2(x, y) - end;
                float angle2 = Vector2.Angle(-dir1, dir3);
                if (angle2 > 90f)
                {
                    return false;
                }
                var dis = dir2.magnitude * Mathf.Sin(angle1 * Mathf.Deg2Rad);
                if (dis > width / 2)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
