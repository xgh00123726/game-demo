using GameBase.Math;
using UnityEngine;

namespace GameBase.Tools
{
    public class GizmosAppend
    {
        public const int CIRCLE_LINE = 25;

        public static void DrawShape(IShape2D shape, float y)
        {
            if (shape is GMath.Circle circle)
            {
                DrawCircle(circle, y);
            }
            if (shape is GMath.Rect2D rect)
            {
                DrawRect(rect, y);
            }
        }

        public static void DrawCircle(GMath.Circle circle, float y = 0)
        {
            Vector3 first = Vector3.zero;
            Vector3 begin = Vector3.zero;
            Vector3 end = Vector3.one;
            for (int i = 0; i < CIRCLE_LINE; i++)
            {
                float alpha = Mathf.PI * 2 / CIRCLE_LINE * i;
                float x = circle.r * Mathf.Cos(alpha) + circle.c.x;
                float z = circle.r * Mathf.Sin(alpha) + circle.c.y;
                end = new Vector3(x, y, z);
                if (i == 0)
                {
                    first = end;
                }
                else
                {
                    Gizmos.DrawLine(begin, end);
                }
                begin = end;
            }
            Gizmos.DrawLine(end, first);
        }

        public static void DrawRect(GMath.Rect2D rect, float y = 0)
        {
            float xmax = rect.rect.xMax;
            float ymax = rect.rect.yMax;
            float xmin = rect.rect.xMin;
            float ymin = rect.rect.yMin;
            Vector3 p1 = new Vector3(xmax, y, ymax);
            Vector3 p2 = new Vector3(xmin, y, ymax);
            Vector3 p3 = new Vector3(xmin, y, ymin);
            Vector3 p4 = new Vector3(xmax, y, ymin);

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }
    }
}
