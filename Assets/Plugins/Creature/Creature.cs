using GameBase.AI;
using GameBase.Buffs;
using GameBase.EntitySystem;
using GameBase.Inventorys;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Triggers;
using GameBase.Spells;
using GameBase.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Creatures
{
    public enum CreatureTag
    {
        None = 0,
        CommonCreature = 1 << 0,
        Player = 1 << 1,
    }

    public class Creature : IUEntity<GameObject>,
        IPoolable,
        ITriggerOwner,
        ITriggerTarget,
        IHealthBarOwner,
        IModifieder,
        ISpeller,
        IBuffOwner,
        IMover,
        IRotater
    {
        private Dictionary<string, float> _possibleAttr = new();

        public float radius = 0.3f;
        public CreatureTag tag;
        public Vector3 healthBarOffset = new Vector3(0, 1.6f, 0);
        public Action OnRelease;
       
        public DynInventory<Spell> spells = new();
        public Modifyables modifyables = new();
        public CommonInventory<Buff> equipments = new() { Size = 6 };
        public List<Buff> buffs = new();

        public int instanceID;
        public BaseAI ai;
        public Mover mover;
        public Rotater rotater;
        public HealthBar healthBar;
        public Animator animator;
        public GameBase.Move.Collider collider;

        public bool Alive { get; internal protected set; }
        public int InstanceID => instanceID;
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + healthBarOffset;

        Vector3 ITriggerTarget.Center => Obj.transform.position;

        float ITriggerTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => modifyables["currHP"];

        float IHealthBarOwner.MaxHP => modifyables["maxHP"];

        bool IHealthBarOwner.ALive => Alive;

        Vector3 ITriggerOwner.HandPosition => Obj.transform.position + new Vector3(0, 1, 0);

        float ISpeller.CoolingAccelerate => modifyables["coolingAccelerate"];

        public Vector3 Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }

        float IMover.Speed => modifyables["moveSpeed"];

        Vector3 IMover.Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }

        float IRotater.Speed => modifyables["rotateSpeed"];

        GameObject IRotater.Obj => Obj;

        bool IRotater.IsRotating { get; set; }

        public Vector3 Dir { get; set; }

        float IMover.Radius => radius;

        Move.Collider IMover.Collider => collider;

        Modifyables IModifieder.Modifyables => modifyables;

        void IPoolable.AfterGet()
        {
            OnRelease = null;
            tag = CreatureTag.CommonCreature;
        }

        void IPoolable.BeforeRelease()
        {
            OnRelease?.Invoke();
        }

        public bool HasPossibleAttr(string name)
        {
            return _possibleAttr.ContainsKey(name);
        }

        public void AddPossibleAttr(string name)
        {
            _possibleAttr.Add(name, default);
        }

        public void SetPossibleAttr(string name, float value)
        {
            _possibleAttr[name] = value;
        }

        public float GetPossibleAttr(string name)
        {
            return _possibleAttr[name];
        }

        public void AddModifier(int modifierID)
        {
            var modifyInfo = ModifierDataBase.Instance[modifierID];
            var modifier = ModifyerSys.Instance.NewEntity();
            modifier.value = modifyInfo.value;
            modifier.type = modifyInfo.type1 | modifyInfo.type2;
            modifyables.ModifySet(modifyInfo.key, modifier);
        }

        public void AddBuff(int id, float duration = 10)
        {
            BuffFactory.Get(id).AddTo(this, duration);
        }

        public void AddAI(AIType type)
        {
            AIFactory.Get(type).AddTo(this);
        }

        public bool AddEquipment(int id, int index)
        {
            var info = BuffDataBase.Instance[id];
            if (info.type == BuffType.Equipment)
            {
                var buff = BuffFactory.Get(id);
                buff.AddTo(this);
                equipments[index] = buff;
                return true;
            }

            return false;
        }

        public bool HasEquipment(int index)
        {
            return equipments.HasItem(index);
        }

        public void RemoveEquipment(int index)
        {
            if (!HasEquipment(index))
            {
                return;
            }
            GetEquipment(index).Remove();
            equipments.Remove(index);
        }

        public Buff GetEquipment(int index)
        {
            return equipments[index];
        }

        public void AddSpell(Spell spell)
        {
            spells.Add(spell);
            spell.speller = this;
        }

       void IBuffOwner.OnGetBuff(Buff buff)
        {
            buffs.Add(buff);
        }

        void IBuffOwner.OnRemoveBuff(Buff buff)
        {
            buffs.Remove(buff);
        }

        public void AddCollider()
        {
            collider = CollideSys.Instance.NewEntity();
            collider.owner = Obj.transform;
            collider.r = radius;
        }
    }
}
