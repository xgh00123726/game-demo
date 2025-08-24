using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Circle : IShape2D
        {
            private float _size;
            public Vector2 c;
            public float r;

            float IShape2D.Size
            {
                get => _size;
                set
                {
                    float factor = value / _size;
                    r *= factor;
                    _size = value;
                }
            }
            Vector2 IShape2D.Center
            {
                get => c;
                set => c = value;
            }
            Vector2 IShape2D.Dir
            {
                get => Vector2.zero;
                set { }
            }

            public Circle(Vector2 c, float r)
            {
                _size = 1f;
                this.c = c;
                this.r = r;
            }

            public bool Contains(float x, float y)
            {
                float dx = x - c.x;
                float dy = y - c.y;
                return dx * dx + dy * dy < r * r;
            }
        }
    }
}
