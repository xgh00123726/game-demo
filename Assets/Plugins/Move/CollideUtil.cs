using GameBase.Math;
using GameBase.Move;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideUtil
{
    public static void CircleColldeToMathCircle(CircleCollider c1, GMath.Circle c2)
    {
        var x1 = c1.position.x;
        var y1 = c1.position.y;
        var x2 = c2.c.x;
        var y2 = c2.c.y;
        var r1 = c1.r;
        var r2 = c2.r;

        float dx = x2 - x1;
        float dy = y2 - y1;
        var force = new Vector2(dx, dy);
        var forceLen = force.magnitude;
        float intersectLen = r1 + r2 - forceLen;

        if (intersectLen > 0)
        {
            c1.isCollide = true;

            c1.force += force.normalized * -intersectLen;
        }
    }
    public static void CircleCollideToCircle(CircleCollider c1, CircleCollider c2)
    {
        var x1 = c1.position.x;
        var y1 = c1.position.y;
        var x2 = c2.position.x;
        var y2 = c2.position.y;
        var r1 = c1.r;
        var r2 = c2.r;

        float dx = x2 - x1;
        float dy = y2 - y1;
        var force = new Vector2(dx, dy);
        var forceLen = force.magnitude;
        float intersectLen = r1 + r2 - forceLen;

        if (intersectLen > 0)
        {
            c1.isCollide = true;
            c2.isCollide = true;

            c1.force += force.normalized * -intersectLen;
            c2.force += force.normalized * intersectLen;
        }
    }

    private enum CollideDir
    {
        None = 0,
        Left = 1 << 0, 
        Right = 1 << 1,
        Top = 1 << 2, 
        Bottom = 1 << 3,
    }
    public static void CircleCollideToRect(CircleCollider cc, RectCollider rc)
    {
        var cx = cc.position.x;
        var cy = cc.position.y;
        var rx = rc.position.x;
        var ry = rc.position.y;
        var w = rc.w;
        var h = rc.h;
        var r = cc.r;

        var rXMin = rc.xMin;
        var rYMin = rc.yMin;
        var rXMax = rc.xMax;
        var rYMax = rc.yMax;

        // 矩形向外扩展一个圆形半径的范围，仅当圆形碰撞器在该范围内时，才会产生碰撞
        var outXMin = rXMin - r;
        var outXMax = rXMax + r;
        var outYMin = rYMin - r;
        var outYMax = rYMax + r;


        if (cx < outXMin || cx > outXMax || cy < outYMin || cy > outYMax)
        {
            return;
        }
        CollideDir dir = CollideDir.None;

        if (cx > rXMin && cx < rXMax)
        {
            if (cy < rYMin)
            {
                dir = CollideDir.Bottom;
            }
            else if (cy > rYMax)
            {
                dir = CollideDir.Top;
            }
        }
        else if (cy > rYMin && cy < rYMax)
        {
            if (cx < rXMin)
            {
                dir = CollideDir.Left;
            }
            else if (cx > rXMax)
            {
                dir = CollideDir.Right;
            }
        }
        else
        {
            if (GMath.CircleContains(cx, cy, r, rXMin, rYMin))
            {
                dir = CollideDir.Bottom | CollideDir.Left;
            }
            else if (GMath.CircleContains(cx, cy, r, rXMin, rYMax))
            {
                dir = CollideDir.Top | CollideDir.Left;
            }
            else if (GMath.CircleContains(cx, cy, r, rXMax, rYMin))
            {
                dir = CollideDir.Bottom | CollideDir.Right;
            }
            else if (GMath.CircleContains(cx, cy, r, rXMax, rYMax))
            {
                dir = CollideDir.Top | CollideDir.Right;
            }
        }

        if (dir != CollideDir.None)
        {
            cc.isCollide = true;
            rc.isCollide = true;
        }

        if ((dir & CollideDir.Left) != 0)
        {
            cc.force += new Vector2(-Mathf.Abs(outXMin - cx), 0);
        }
        if ((dir & CollideDir.Right) != 0)
        {
            cc.force += new Vector2(Mathf.Abs(outXMax - cx), 0);
        }
        if ((dir & CollideDir.Bottom) != 0)
        {
            cc.force += new Vector2(0, -Mathf.Abs(outYMin - cy));
        }
        if ((dir & CollideDir.Top) != 0)
        {
            cc.force += new Vector2(0, Mathf.Abs(outYMax - cy));
        }
    }

    public static void CircleCollideToFCRect(CircleCollider cc, FCRectCollider rc)
    {
        var cx = cc.position.x;
        var cy = cc.position.y;
        var rx = rc.position.x;
        var ry = rc.position.y;
        var w = rc.w;
        var h = rc.h;
        var r = cc.r;
        var fcr = rc.filletedCornerR;

        var rXMin = rc.xMin;
        var rYMin = rc.yMin;
        var rXMax = rc.xMax;
        var rYMax = rc.yMax;

        // 矩形向外扩展一个圆形半径的范围，仅当圆形碰撞器在该范围内时，才会产生碰撞
        var outXMin = rXMin - r;
        var outXMax = rXMax + r;
        var outYMin = rYMin - r;
        var outYMax = rYMax + r;


        if (cx < outXMin || cx > outXMax || cy < outYMin || cy > outYMax)
        {
            return;
        }
        CollideDir dir = CollideDir.None;

        if (cx > rXMin + fcr && cx < rXMax - fcr)
        {
            if (cy < rYMin)
            {
                dir = CollideDir.Bottom;
            }
            else if (cy > rYMax)
            {
                dir = CollideDir.Top;
            }
        }
        else if (cy > rYMin + fcr && cy < rYMax - fcr)
        {
            if (cx < rXMin)
            {
                dir = CollideDir.Left;
            }
            else if (cx > rXMax)
            {
                dir = CollideDir.Right;
            }
        }
        else
        {
            if (GMath.CircleContains(cx, cy, r, rXMin, rYMin))
            {
                CircleColldeToMathCircle(cc, rc.LeftBottomCircle);
                cc.isCollide = true;
                rc.isCollide = true;
            }
            else if (GMath.CircleContains(cx, cy, r, rXMin, rYMax))
            {
                CircleColldeToMathCircle(cc, rc.LeftTopCircle);
                cc.isCollide = true;
                rc.isCollide = true;
            }
            else if (GMath.CircleContains(cx, cy, r, rXMax, rYMin))
            {
                CircleColldeToMathCircle(cc, rc.RightBottomCircle);
                cc.isCollide = true;
                rc.isCollide = true;
            }
            else if (GMath.CircleContains(cx, cy, r, rXMax, rYMax))
            {
                CircleColldeToMathCircle(cc, rc.RightTopCircle);
                cc.isCollide = true;
                rc.isCollide = true;
            }
        }

        if (dir != CollideDir.None)
        {
            cc.isCollide = true;
            rc.isCollide = true;
        }

        if ((dir & CollideDir.Left) != 0)
        {
            cc.force += new Vector2(-Mathf.Abs(outXMin - cx), 0);
        }
        if ((dir & CollideDir.Right) != 0)
        {
            cc.force += new Vector2(Mathf.Abs(outXMax - cx), 0);
        }
        if ((dir & CollideDir.Bottom) != 0)
        {
            cc.force += new Vector2(0, -Mathf.Abs(outYMin - cy));
        }
        if ((dir & CollideDir.Top) != 0)
        {
            cc.force += new Vector2(0, Mathf.Abs(outYMax - cy));
        }
    }
}
