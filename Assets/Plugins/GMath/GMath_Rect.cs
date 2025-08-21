using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Rect2D : IShape2D
        {
            private float _size;
            public Rect rect;
            public Rect2D(Rect rect)
            {
                _size = 1f;
                this.rect = rect;
            }

            public Rect2D(float width, float height)
            {
                _size = 1f;
                rect = Rect.MinMaxRect(0, 0, width, height);
            }

            public Rect2D(float xmin, float ymin, float xmax, float ymax)
            {
                _size = 1f;
                rect = Rect.MinMaxRect(xmin, ymin, xmax, ymax);
            }

            float IShape2D.Size
            {
                get => _size;
                set
                {
                    float factor = value / _size;
                    Vector2 center = rect.center;
                    rect.width = rect.width * factor;
                    rect.height = rect.height * factor;
                    rect.center = center;
                    _size = value;
                }
            }
            Vector2 IShape2D.Center
            {
                get => rect.center;
                set => rect.center = value;
            }

            public bool Contains(float x, float y)
            {
                return rect.Contains(new Vector2(x, y));
            }
        }
    }
}
