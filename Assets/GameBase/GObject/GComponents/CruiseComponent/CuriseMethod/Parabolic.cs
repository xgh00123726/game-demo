using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Object
{
    // 抛物线轨迹
    // @amp: 幅度
    // @speed: 速度（xz二维平面投影速度）
    public class Parabolic : CuriseBase
    {
        public float amp = 2f;

        private float _yFactor = 0f;

        public override void ReInit()
        {
            base.ReInit();
            _yFactor = 1 / Mathf.Max((end - start).sqrMagnitude, Mathf.Epsilon) / 4 * amp;
        }

        public override Vector3 TrackEquation(float time)
        {
            float x = speed * time;
            float y = ((end - start).magnitude * x - x * x) * _yFactor;

            return _dir * speed * time + start + new Vector3(0, y, 0);
        }
    }
}
