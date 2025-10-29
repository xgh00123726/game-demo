using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Circle : IShape2D
        {
            public Vector2 c;
            public float r;
            public Vector2 dir;
            Vector2 IShape2D.Center
            {
                get => c;
                set => c = value;
            }
            Vector2 IShape2D.Dir
            {
                get => dir;
                set => dir = value;
            }
            float IShape2D.Size
            {
                get => r;
                set => r = value;
            }

            public Circle(float r)
            {
                this.c = Vector2.zero;
                this.r = r;
                dir = Vector2.zero;
            }

            public Circle(Vector2 c, float r)
            {
                this.c = c;
                this.r = r;
                dir = Vector2.zero;
            }

            public bool Contains(float x, float y)
            {
                float dx = x - c.x;
                float dy = y - c.y;
                return dx * dx + dy * dy < r * r;
            }
        }
    
        public static bool CircleContains(float cx, float cy, float r, float x, float y)
        {
            float dx = cx - x;
            float dy = cy - y;
            return dx * dx + dy * dy <= r * r;
        }
    }
}
