using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Math
{
    public interface IShape2D
    {
        // 坐标(x,y)是否在shape内
        bool Contains(Vector3 position);
        Vector3 Center { set; get; }
        Vector3 Dir {  set; get; }
        float Size { set; get; }
    }
}
