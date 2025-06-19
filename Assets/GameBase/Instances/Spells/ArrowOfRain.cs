using UnityEngine;
using GameBase.Projectile;
using GameBase.Resources;
using GameBase.Effects;
using GameBase.GCamera;
using GameBase.Object;

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
            coolingTimeSet = 5f;
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
            for (int i = 0; i < 5; ++i)
            {
                var projectile = ProjectileMgr.Instance.CreateProjectile<Arrow>();
                projectile.transform.position = src;

                var curise = new Parabolic();
                curise.speed = 5f;
                projectile.Curise = curise;

                projectile.Dest = dest + new Vector3(Random.Range(-_radius, _radius), 0, Random.Range(-_radius, _radius));
            }
        }
    }
}
