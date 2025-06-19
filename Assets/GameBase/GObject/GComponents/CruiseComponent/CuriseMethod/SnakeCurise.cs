using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Object
{
    // 蛇形轨迹（正弦曲线）
    // @amp: 幅度
    // @freq: 频率乘数
    // @speed: xz平面投影的速度
    public class SnakeCurise : CuriseBase
    {
        public float amp = 2.0f;
        public float freq = 2.0f;
        public override Vector3 TrackEquation(float time)
        {
            return _dir * speed * time + start + new Vector3(0, Mathf.Sin(time * freq) * amp, 0);
        }
    }
}
