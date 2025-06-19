using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public struct Circle
        {
            private Vector2 _center;
            public Vector2 center
            {
                get
                {
                    return _center;
                }
                set
                {
                    _center = value;
                }
            }
            public Vector2 c
            {
                get
                {
                    return _center;
                }
                set
                {
                    _center = value;
                }
            }
            private float _radius;
            public float r
            {
                get
                {
                    return _radius;
                }
                set
                {
                    _radius = value;
                }
            }
            public float radius
            {
                get
                {
                    return _radius;
                }
                set
                {
                    _radius = value;
                }
            }
            public Circle(Vector2 center, float radius)
            {
                _center = center;
                _radius = radius;
            }

            public bool Contains(Vector2 point)
            {
                return (point - _center).magnitude <= _radius;
            }
        }


        public static bool IsIntersect(Circle c1, Circle c2)
        {
            return (c1.c - c2.c).magnitude < c1.r + c2.r;
        }

        public static bool IsIntersect(Circle c, Rect r)
        {
            Vector2 v = new(Mathf.Abs(c.center.x - r.center.x), Mathf.Abs(c.center.y - r.center.y));
            Vector2 h = new(r.width / 2, r.height / 2);
            Vector2 u = v - h;
            u.x = Mathf.Max(u.x, 0);
            u.y = Mathf.Max(u.y, 0);
            return u.magnitude < c.radius;
        }

        public static bool IsIntersect(Rect r, Circle c)
        {
            return IsIntersect(c, r);
        }
    }
}
