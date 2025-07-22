using System;
using System.Collections;
using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public abstract class CurveBase
    {
        public ProjectileObject _projectile;
        public float speed = 1f;
        
        public CurveBase(ProjectileObject projectile)
        {
            _projectile = projectile;
        }
        /// <summary>
        /// 方向更新
        /// </summary>
        public abstract void DirUpdate();
        public virtual void PosUpdate()
        {
            _projectile.transform.position = _projectile.transform.position + _projectile.Dir * speed * Time.deltaTime;
        }
    }
}
