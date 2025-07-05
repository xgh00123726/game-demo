using System;
using System.Collections;
using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public abstract class CurveBase
    {
        public ICurvableProjectile _projectile;
        public float speed = 1f;
        public CurveBase(ICurvableProjectile projectile)
        {
            _projectile = projectile;
        }
        public abstract void DirUpdate();
        public virtual void PosUpdate()
        {
            _projectile.Transform.position = _projectile.Transform.position + _projectile.Dir * speed * Time.deltaTime;
        }
    }
}
