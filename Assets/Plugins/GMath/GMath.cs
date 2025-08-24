using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        /// <summary>
        /// 返回两个线段的夹角
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float AngleOfLines2D(float x1, float y1, float x2, float y2)
        {
            return Mathf.Acos(
                (x1 * x2 + y1 * y2) / Mathf.Sqrt((x1 * x1 + y1 * y1) * (x2 * x2 + y2 * y2))
                ) * Mathf.Rad2Deg;
        }

        public static Vector2 VerticalVector2(Vector2 v)
        {
            return new Vector2(-v.y, v.x);
        }

        public static float GameDistance(Vector3 c1, Vector3 c2)
        {
            float x1 = c1.x;
            float y1 = c1.z;
            float x2 = c2.x;
            float y2 = c2.z;
            float dx = x1 - x2;
            float dy = y1 - y2;
            return Mathf.Sqrt(dx * dx + dy * dy);
        }


        public static bool FloatEqualZero(float val)
        {
            return val > -Mathf.Epsilon && val < Mathf.Epsilon;
        }

        public static bool FloatEqual(float val1, float val2)
        {
            return val1 > val2 - Mathf.Epsilon && val1 < val2 + Mathf.Epsilon;
        }

        // if float value equal to defaultValue, then return null, else return value
        public static float? FloatToFloatnull(float val, float defaultValue)
        {
            if (FloatEqual(val, defaultValue))
            {
                return null;
            }
            return val;
        }

        public static float? FloatToFloatnull(float val)
        {
            if (FloatEqual(val, 0f))
            {
                return null;
            }
            return val;
        }

        public static bool IsIntersect(Rect r1, Rect r2)
        {
            Vector2 diff = r1.center - r2.center;
            return r1.width + r2.width < diff.x || r2.height + r1.height < diff.y;
        }

        public static int BoolToInt(bool b)
        {
            return b ? 1 : 0;
        }
    }

}
