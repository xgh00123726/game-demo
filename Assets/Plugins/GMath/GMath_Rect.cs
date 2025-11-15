using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Rect2D : IShape2D
        {
            public Vector3 dir;
            public Rect rect;
            public Rect2D(Rect rect)
            {
                this.rect = rect;
                dir = Vector3.zero;
            }

            public Rect2D(float width, float height)
            {
                rect = Rect.MinMaxRect(0, 0, width, height);
                dir = Vector3.zero;
            }

            public Rect2D(float xmin, float ymin, float xmax, float ymax)
            {
                rect = Rect.MinMaxRect(xmin, ymin, xmax, ymax);
                dir = Vector2.zero;
            }

            Vector3 IShape2D.Center
            {
                get => rect.center;
                set => rect.center = value;
            }
            Vector3 IShape2D.Dir
            {
                get => dir;
                set => dir = value;
            }

            float IShape2D.Size
            {
                get => rect.width;
                set
                {
                    rect.height = value / rect.width * rect.height;
                    rect.width = value;
                }
            }

            bool IShape2D.Contains(Vector3 position)
            {
                return true;
            }

            public bool Contains(float x, float y)
            {
                return rect.Contains(new Vector2(x, y));
            }
        }
    }
}
