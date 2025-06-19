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
            var projectile = ProjectileMgr.Instance.CreateProjectile<Arrow>();
            projectile.transform.position = src;
            
            var snake = new SnakeCurise();
            snake.amp = 2;
            snake.freq = 4;
            projectile.Curise = snake;

            dest.y = projectile.transform.position.y;
            projectile.Dest = dest;
        }
    }
}
