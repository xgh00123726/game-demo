using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public interface IShape2D
    {
        // 返回该形状空间的坐标(x,y)处对应的世界坐标
        // 注：-1<=x,y<=1
        Vector2 ShapeSpcaceToWorld(float x, float y);
        // 坐标(x,y)是否在shape内
        bool IsInShape(float x, float y);
    }
}
