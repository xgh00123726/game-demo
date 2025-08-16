using Combines.Projectiles;
using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Spells;
using GameBase.UI;
using UnityEngine;

namespace Instance.Creatures
{
    public class GameCreature : Creature,
        IProjectileOwner,
        IProjectileTarget,
        IHealthBarOwner,
        IModifyOwner<float>,
        ISpeller,
        IBuffOwner,
        IMover
    {
        public GameCreature()
        {
            _container["currHP"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f);
            _container["maxHP"] = ModifyableSys<float>.Instance.NewEntity<Modifyable<float>>(100f);

            AfterInstantiateObj = () =>
            {
                healthbar = HealthBarSys.Instance.NewEntity<HealthBar>();
                healthbar.ObjID = 6;
                healthbar.owner = this;
            };
        }


        public HealthBar healthbar;
        protected ModifyableContainer<float> _container = new();
        protected BuffContainer _buffContainer = new();
        public override bool ReleaseTrigger => _container["currHP"].Value <= 0f;

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + new Vector3(0, 1, 1);

        Vector3 IProjectileTarget.Center => Obj.transform.position;

        float IProjectileTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => _container["currHP"].Value;

        float IHealthBarOwner.MaxHP => _container["maxHP"].Value;

        bool IHealthBarOwner.ALive => Alive;

        ModifyableContainer<float> IModifyOwner<float>.Modifyables => _container;

        Vector3 IProjectileOwner.HandPosition => Obj.transform.position + new Vector3(0, 1, 0);

        float ISpeller.CoolingAccelerate => throw new System.NotImplementedException();

        Vector3 ISpeller.Position => Obj.transform.position;

        BuffContainer IBuffOwner.Buffs => _buffContainer;

        float IMover.moveSpeed => _container["moveSpeed"].Value;

        GameObject IMover.GO => Obj;
    }
}
