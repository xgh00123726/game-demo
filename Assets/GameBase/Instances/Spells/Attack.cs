using GameBase.Object;
using GameBase.Projectile;
using GameBase.GCamera;
using UnityEngine;

namespace GameBase.Spell
{
    public class Attack : GSpell
    {
        public float indicatorRadius = 0.6f;
        public Vector3 src;
        public Vector3 dest;
        public delegate void AttackInvokeAction(Attack spell);
        public AttackInvokeAction AttackAction;

        public Attack(ISpeller speller) : base(speller) { }

        protected override void OnCast()
        {
            AttackAction?.Invoke(this);
            var projectile = ProjectileMgr<Arrow>.Instance.CreateProjectile();
            projectile.SetTrack(projectile.transform.position, dest);
            projectile.transform.position = src;
        }
    }
}
