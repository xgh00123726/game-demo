using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Line : IShape2D
        {
            public float width;
            public Vector3 begin;
            public Vector3 end;
            public float pivot;

            public Line(Vector3 begin, Vector3 end)
            {
                this.begin = begin;
                this.end = end;
                width = 0;
                pivot = 0.5f;
            }

            public Line(Vector3 begin, Vector3 dir, float length)
            {
                this.begin = begin;
                this.end = begin + dir.normalized * length;
                width = 0;
                pivot = 0.5f;
            }

            public Line(float length)
            {
                begin = Vector3.zero;
                end = begin + Vector3.one.normalized * length;
                width = 0;
                pivot = 0.5f;
            }

            public Vector3 Dir
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
            public Vector3 LeftBottom => begin - GMath.VerticalVector2(Dir).normalized * width / 2;
            public Vector3 LeftTop => end - GMath.VerticalVector2(Dir).normalized * width / 2;
            public Vector3 RightBottom => begin + GMath.VerticalVector2(Dir).normalized * width / 2;
            public Vector3 RightTop => end + GMath.VerticalVector2(Dir).normalized * width / 2;
            Vector3 IShape2D.Center
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

            public bool Contains(Vector3 position)
            {
                float x = position.x;
                float y = position.z;
                var dir1_3 = end - begin;
                var dir1 = new Vector2(dir1_3.x, dir1_3.z);
                var dir2 = new Vector2(x, y) - new Vector2(begin.x, begin.z);
                float angle1 = Vector2.Angle(dir1, dir2);
                if (angle1 > 90f)
                {
                    return false;
                }
                var dir3 = new Vector2(x, y) - new Vector2(end.x, end.z);
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
