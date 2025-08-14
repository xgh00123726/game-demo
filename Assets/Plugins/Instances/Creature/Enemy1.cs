using Combines.Projectiles;
using GameBase.Modify;
using GameBase.UI;
using UnityEngine;

namespace GameBase.Instance
{
    public class Enemy1 : Creature.Creature,
        IProjectileTarget,
        IHealthBarOwner,
        IModifyOwner<float>
    {
        public HealthBar healthbar;
        protected ModifyableContainer<float> _container = new();
        public float maxHP = 100f;

        public Enemy1()
        {
            AfterInstantiateFromPoolDelegate = () => 
            { 
                healthbar = HealthBarSys.Instance.NewEntity<HealthBar>();
                healthbar.ObjID = 6;
                healthbar.owner = this;
            };
            _container["currHP"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f);
        }

        public override bool ReleaseTrigger => _container["currHP"].Value <= 0f;

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + new Vector3(0, 1, 1);

        Vector3 IProjectileTarget.Center => Obj.transform.position;

        float IProjectileTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => _container["currHP"].Value;

        float IHealthBarOwner.MaxHP => maxHP;

        bool IHealthBarOwner.ALive => Alive;

        ModifyableContainer<float> IModifyOwner<float>.Modifyables => _container;

        //void IProjectileTarget.GetDamage(float damage)
        //{
        //    currHP -= damage;
        //    HPChange = true;
        //    var text = TextSys.Instance.NewEntity<FloatText>();
        //    text.value = damage.ToString();
        //    text.showPosition = Obj.transform.position;
        //}
    }
}
