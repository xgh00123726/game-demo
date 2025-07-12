using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Projectile
{
    // 抛物线轨迹
    // @amp: 幅度
    // @speed: 速度（xz二维平面投影速度）
    public class Parabolic : CurveBase
    {
        public float amp = 2f;

        public Parabolic(ICurvableProjectile projectile) : base(projectile)
        {
        }

        public override void DirUpdate()
        {
            
        }
    }
}
