using GameBase.Animations;
using GameBase.Buffs;
using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.UI;
using UnityEngine;

namespace GameBase.Creatures
{
    public enum Tag
    {
        CommonCreature = 1 << 0,
        Player = 1 << 1,
    }
    public class Creature : IUEntity<GameObject>,
        IPoolable,
        IProjectileOwner,
        IProjectileTarget,
        IHealthBarOwner,
        IModifyOwner<float>,
        ISpeller,
        IBuffOwner,
        IMover,
        IRotater,
        IPlayerAnimable
    {
        public int radius;
        public Tag tag;
        public bool Alive { get; internal protected set; }
        public int InstanceID { get; set; }
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        public virtual bool ReleaseTrigger => _modifyableContainer["currHP"].Value <= 0f;

        protected ModifyableContainer<float> _modifyableContainer = new();
        protected SpellContainer _spellContainer = new();
        protected BuffContainer _buffContainer = new();

        public Animator animator;

        public ModifyableContainer<float> ModifyableContainer => _modifyableContainer;
        public BuffContainer BuffContainer => _buffContainer;

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + new Vector3(0, 1, 1);

        Vector3 IProjectileTarget.Center => Obj.transform.position;

        float IProjectileTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => _modifyableContainer["currHP"].Value;

        float IHealthBarOwner.MaxHP => _modifyableContainer["maxHP"].Value;

        bool IHealthBarOwner.ALive => Alive;

        ModifyableContainer<float> IModifyOwner<float>.Modifyables => _modifyableContainer;

        Vector3 IProjectileOwner.HandPosition => Obj.transform.position + new Vector3(0, 1, 0);

        float ISpeller.CoolingAccelerate => _modifyableContainer["coolingAccelerate"].Value;

        public Vector3 Position
        {
            get => Obj.transform.position;
            set
            {
                Obj.transform.position = value;
                Dest = value;
            }
        }

        BuffContainer IBuffOwner.Buffs => _buffContainer;

        float IMover.Speed => _modifyableContainer["moveSpeed"].Value;

        GameObject IMover.GO => Obj;

        float IRotater.Speed => _modifyableContainer["rotateSpeed"].Value;

        GameObject IRotater.GO => Obj;

        public bool IsMoving { get; set; }
        public bool IsRotating { get; set; }

        Animator IPlayerAnimable.Animator => animator;

        public Vector3 Dest { get; set; }

        public Vector3 Dir { get; set; }

        public SpellContainer SpellContainer => _spellContainer;

        bool IPlayerAnimable.IsMoving()
        {
            return IsMoving;
        }

        bool IPlayerAnimable.IsIdle()
        {
            return !IsMoving;
        }

        public virtual void AfterGet()
        {
            tag = Tag.CommonCreature;
        }

        public void BeforeRelease()
        {
            
        }
    }
}
