using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public static class GPos
    {
        public static Vector3 Vec2ToVec3(Vector2 pos, float height)
        {
            return new Vector3(pos.x, height, pos.y);
        }
        public static Vector2 Vec3ToVec2(Vector3 pos)
        {
            return new Vector2(pos.x, pos.z);
        }
        public static float DisVec2Sqr(Vector3 pos1, Vector3 pos2)
        {
            float xx = pos1.x - pos2.x;
            float zz = pos1.z - pos2.z;
            return xx * xx + zz * zz;
        }
        public static float DisVec2(Vector3 pos1, Vector3 pos2)
        {
            float xx = pos1.x - pos2.x;
            float zz = pos1.z - pos2.z;
            return (float)System.Math.Sqrt(xx * xx + zz * zz);
        }
    }
}
