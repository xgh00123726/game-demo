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
using GameBase.Tools;

namespace GameBase.Creatures
{
    public enum CreatureTag
    {
        None = 0,
        CommonCreature = 1 << 0,
        Player = 1 << 1,
        ALL = 0x7FFFFFFF,
    }

    public partial class Creature : IKeyEntity<int>,
        ITriggerOwner,
        ITriggerTarget,
        IHealthBarOwner,
        IModifieder,
        IBuffOwner,
        IMover
    {
        public float radius = 0.3f;
        public CreatureTag tag;
        public Vector3 healthBarOffset = new Vector3(0, 1.6f, 0);
        public Action<Creature> OnDead;
       
        public List<Buff> buffs = new();

        public int instanceID;
        public BaseAI ai;
        public Mover mover;
        public HealthBar healthBar;
        public Animator animator;
        public GameBase.Move.CircleCollider collider;

        public bool Alive { get; internal protected set; }
        public GameObject obj;
        public int Key { get; set; }

        Vector3 IHealthBarOwner.HealthBarPosition => obj.transform.position + healthBarOffset;

        Vector3 ITriggerTarget.Center => obj.transform.position;

        float ITriggerTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => modifyables["currHP"].Value;

        float IHealthBarOwner.MaxHP => modifyables["maxHP"].Value;

        bool IHealthBarOwner.ALive => Alive;

        public Vector3 HandPosition => obj.transform.position + new Vector3(0, 1, 0);

        public Vector3 Position
        {
            get => obj.transform.position;
            set => obj.transform.position = value;
        }

        float IMover.MoveSpeed => modifyables["moveSpeed"].Value;

        Vector3 IMover.Position
        {
            get => obj.transform.position;
            set => obj.transform.position = value;
        }

        float IMover.RotateSpeed => modifyables["rotateSpeed"].Value;

        GameObject IMover.Obj => obj;

        public Vector3 Dir { get; set; }

        float IMover.Radius => radius;

        Move.CircleCollider IMover.Collider => collider;

        Modifyables IModifieder.Modifyables => modifyables;


        public void AddBuff(int id, float duration = -1)
        {
            BuffFactory.Get(id).AddTo(this, duration);
        }

        public void AddAI(AIType type)
        {
            AIFactory.Get(type).AddTo(this);
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
            collider = CollideSys.Instance.NewEntity<CircleCollider>();
            collider.Owner = obj.transform;
            collider.r = radius;
        }
    }
}
