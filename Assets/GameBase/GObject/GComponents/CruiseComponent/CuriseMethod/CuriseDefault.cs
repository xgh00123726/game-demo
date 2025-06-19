using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Object
{
    // 直线轨迹
    // @speed: 速度
    public class CuriseDefault : CuriseBase
    {
        public override Vector3 TrackEquation(float time)
        {
            return speed * time * _dir + start;
        }
    }
}
