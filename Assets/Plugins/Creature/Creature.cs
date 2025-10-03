using GameBase.Animations;
using GameBase.Buffs;
using GameBase.EntitySystem;
using GameBase.Inventorys;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.UI;
using System;
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
        IModifieder,
        ISpeller,
        IBuffOwner,
        IMover,
        IRotater,
        IPlayerAnimable,
        IKeySpeller
    {
        public int radius;
        public Tag tag;
        public Action<Buff> OnGetBuffAction;
        public Vector3 healthBarOffset = new Vector3(0, 1.6f, 0);
        public DynInventory<Spell> spells = new();
        public bool Alive { get; internal protected set; }
        public int InstanceID => instanceID;
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        public virtual bool ReleaseTrigger => _fModifyables["currHP"] <= 0f;

        protected Modifyables _fModifyables = new();
        protected CommonInventory<Buff> _equipments = new() { Size = 6 };

        internal int instanceID;
        internal Mover mover;
        internal Rotater rotater;
        internal HealthBar healthBar;
        internal Animator animator;

        public Modifyables Modifyables => _fModifyables;
        public Mover Mover => mover;
        public Rotater Rotater => rotater;
        public HealthBar HealthBar => healthBar;
        public Animator Animator => animator;

        public CommonInventory<Buff> Equipments => _equipments;

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position 
            + (CreatureGizmosDraw.Instance.healthBarDebugMode ? CreatureGizmosDraw.Instance.healthbarOffset : healthBarOffset);

        Vector3 IProjectileTarget.Center => Obj.transform.position;

        float IProjectileTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => _fModifyables["currHP"];

        float IHealthBarOwner.MaxHP => _fModifyables["maxHP"];

        bool IHealthBarOwner.ALive => Alive;

        Vector3 IProjectileOwner.HandPosition => Obj.transform.position + new Vector3(0, 1, 0);

        float ISpeller.CoolingAccelerate => _fModifyables["coolingAccelerate"];

        public Vector3 Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }

        float IMover.Speed => _fModifyables["moveSpeed"];

        Vector3 IMover.Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }

        float IRotater.Speed => _fModifyables["rotateSpeed"];

        GameObject IRotater.GO => Obj;

        public bool IsRotating { get; set; }

        Animator IPlayerAnimable.Animator => animator;

        public Vector3 Dir { get; set; }

        Vector3 IKeySpeller.Position => Obj.transform.position;

        bool IPlayerAnimable.IsMoving()
        {
            return mover.IsMoving;
        }

        bool IPlayerAnimable.IsIdle()
        {
            return !mover.IsMoving;
        }

        public virtual void AfterGet()
        {
            tag = Tag.CommonCreature;
        }

        public void BeforeRelease()
        {
        }

        public void AddBuff(int id, float duration = 10)
        {
            Buffs.BuffFactory.Get(id).AddTo(this, duration);
        }

        public void AddEquipment(int id, int index)
        {
            var buff = Buffs.BuffFactory.Get(id);
            buff.AddTo(this);
            _equipments[index] = buff;
        }

        public bool HasEquipment(int index)
        {
            return _equipments.HasItem(index);
        }

        public void RemoveEquipment(int index)
        {
            if (!HasEquipment(index))
            {
                return;
            }
            GetEquipment(index).Remove();
            _equipments.Remove(index);
        }

        public Buff GetEquipment(int index)
        {
            return _equipments[index];
        }

        Spell IKeySpeller.GetSpell(int index)
        {
            if (spells.HasItem(index))
            {
                return spells[index];
            }

            return null;
        }

        public void OnGetBuff(Buff buff)
        {
            OnGetBuffAction?.Invoke(buff);  
        }
    }
}
