using GameBase.Animations;
using GameBase.Buffs;
using GameBase.EntitySystem;
using GameBase.Inventorys;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Projectiles;
using GameBase.Spells;
using GameBase.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Creatures
{
    public enum CreatureTag
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
        IAnimatable
    {
        public int radius;
        public CreatureTag tag;
        public Vector3 healthBarOffset = new Vector3(0, 1.6f, 0);
        
        public bool Alive { get; internal protected set; }
        public int InstanceID => instanceID;
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        public virtual bool ReleaseTrigger => _fModifyables["currHP"] <= 0f;

        protected DynInventory<Spell> _spells = new();
        protected Modifyables _fModifyables = new();
        protected CommonInventory<Buff> _equipments = new() { Size = 6 };
        protected List<Buff> _buffs = new();

        internal int instanceID;
        internal Mover mover;
        internal Rotater rotater;
        internal HealthBar healthBar;
        internal Animator animator;
        internal HumanAnimController animController;

        public Modifyables Modifyables => _fModifyables;
        public Mover Mover => mover;
        public Rotater Rotater => rotater;
        public HealthBar HealthBar => healthBar;
        public Animator Animator => animator;
        public HumanAnimController AnimController => animController;
        public DynInventory<Spell> Spells => _spells;
        public CommonInventory<Buff> Equipments => _equipments;
        public List<Buff> Buffs => _buffs;

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

        Animator IAnimatable.Animator => animator;

        public Vector3 Dir { get; set; }

        bool IAnimatable.IsMoving()
        {
            return mover.IsMoving;
        }

        bool IAnimatable.IsIdle()
        {
            return !mover.IsMoving;
        }

        void IPoolable.AfterGet()
        {
            tag = CreatureTag.CommonCreature;
        }

        void IPoolable.BeforeRelease()
        {
        }

        public void AddModifier(int modifierID)
        {
            var modifyInfo = ModifierDataBase.Instance[modifierID];
            var modifier = ModifyerSys.Instance.NewEntity();
            modifier.value = modifyInfo.value;
            modifier.type = modifyInfo.type1 | modifyInfo.type2;
            Modifyables.ModifySet(modifyInfo.key, modifier);
        }

        public void AddBuff(int id, float duration = 10)
        {
            BuffFactory.Get(id).AddTo(this, duration);
        }

        public bool AddEquipment(int id, int index)
        {
            var info = BuffDataBase.Instance[id];
            if (info.type == BuffType.Equipment)
            {
                var buff = BuffFactory.Get(id);
                buff.AddTo(this);
                _equipments[index] = buff;
                return true;
            }

            return false;
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

        public void AddSpell(Spell spell)
        {
            _spells.Add(spell);
            spell.speller = this;
        }

       void IBuffOwner.OnGetBuff(Buff buff)
        {
            _buffs.Add(buff);
        }

        void IBuffOwner.OnRemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
        }
    }
}
