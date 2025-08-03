using GameBase.Projectile;
using GameBase.UI;
using UnityEngine;

namespace GameBase.Instance
{
    public class Enemy1 : Creature.Creature,
        IProjectileTarget,
        IHealthBarOwner
    {
        public HealthBar healthbar;
        public float maxHP = 100f;
        public float currHP = 100f;

        public Enemy1()
        {
            healthbar = HealthBarSys.Instance.NewEntity<HealthBar>();
            healthbar.bodyID = 6;
            healthbar.MaxHP = maxHP;
            healthbar.CurrHP = currHP;
            healthbar.owner = this;
        }

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + new Vector3(0, 1, 1);

        Vector3 IProjectileTarget.Center => Obj.transform.position;

        float IProjectileTarget.Radius => radius;

        void IProjectileTarget.GetDamage(float damage)
        {
            currHP -= damage;
            healthbar.CurrHP = currHP;
            var text = TextSys.Instance.NewEntity<FloatText>();
            text.value = damage.ToString();
            text.bodyID = 1;
            text.showPosition = Obj.transform.position;
        }
    }
}
