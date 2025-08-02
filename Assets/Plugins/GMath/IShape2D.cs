using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public interface IShape2D
    {
        // 坐标(x,y)是否在shape内
        bool Contains(float x, float y);
        float Size { get; set; }
        Vector2 Center { set; get; }
    }
}
