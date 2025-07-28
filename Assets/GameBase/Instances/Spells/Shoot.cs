using System;
using UnityEngine;
using GameBase.Projectile;
using System.Collections.Generic;
namespace GameBase.Spell
{
    public class Shoot : GSpell
    {
        private IProjectileTarget _shootTarget;
        public bool _hasShootTarget = false;
        public IProjectileTarget ShootTarget
        {
            get => _shootTarget;
            set
            {
                if (value == null) return;

                _shootTarget = value;
                _hasShootTarget = true;
            }
        }

        public ProjectileObject projectile;
        public LinkedList<ProjectileObject> projectiles;

        protected override void OnCast()
        {
            base.OnCast();


        }
    }
}
