using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public partial class GMath
    {
        public static Rect BoundsRect(Bounds bounds)
        {
            float xmax = bounds.max.x;
            float ymax = bounds.max.z;
            float xmin = bounds.min.x;
            float ymin = bounds.min.z;
            return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
        }

        public static bool BoundsContains(Bounds bounds, Vector3 position)
        {
            float xmax = bounds.max.x;
            float ymax = bounds.max.z;
            float xmin = bounds.min.x;
            float ymin = bounds.min.z;
            float x = position.x;
            float y = position.z;
            return x < xmax && x > xmin && y < ymax && y > ymin;
        }
    }
}
