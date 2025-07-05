using UnityEngine;
using GameBase.Projectile;

namespace GameBase.Spell
{
    public class ArrowOfRain : SpellWithCircleIndicator
    {
        public Vector3 src;
        public Vector3 dest;
        public delegate void AttackInvokeAction(ArrowOfRain spell);
        public AttackInvokeAction AttackAction;

        public ArrowOfRain(ISpeller speller, IIndicatorCircleSpell indicator) : base(speller, indicator)
        {
            coolingTimeSet = 1f;
            Radius = 2f;
        }

        protected override void OnChoose()
        {
            base.OnChoose();
        }

        protected override void OnCast()
        {
            base.OnCast();
            AttackAction?.Invoke(this);
            for (int i = 0; i < 1; ++i)
            {
                var projectile = ProjectileMgr<FireArrow>.Instance.CreateProjectile();
                projectile.transform.position = src;
                Debug.Log("parabolic time limit");
                projectile.Curve = new ParabolicTimeLimit(projectile);
                projectile.SetTrack(src, dest);
            }
        }
    }
}
