using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public enum Axis
        {
            X = 0,
            Y = 1,
            Z = 2,
        };

        /// <summary>
        /// 返回两个线段的夹角
        /// </summary>
        /// <param name="L1"></param>
        /// <param name="L2"></param>
        /// <param name="axisIgnored"></param>
        /// <returns></returns>
        public static float AngleOfLines2D(Vector3 L1, Vector3 L2, Axis axisIgnored)
        {
            if (axisIgnored == Axis.X)
            {
                return AngleOfLines2D(L1.y, L1.z, L2.y, L2.z);
            }
            else if (axisIgnored == Axis.Y)
            {
                return AngleOfLines2D(L1.x, L1.z, L2.x, L2.z);
            }
            else
            {
                return AngleOfLines2D(L1.x, L1.y, L2.x, L2.y);
            }
        }
    }
}
