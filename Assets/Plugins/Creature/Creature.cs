using GameBase.AI;
using GameBase.Buffs;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Triggers;
using GameBase.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Creatures
{
    public partial class Creature : IKeyEntity<string>,
        ITriggerOwner,
        ITriggerTarget,
        IHealthBarOwner,
        IBuffOwner,
        IMover, 
        ITriggerAttach,
        IFlyingTarget
    {
        internal int id;

        public float Radius { get; set; } = 0.3f;
        public Vector3 HealthBarOffset { get; set; } = new Vector3(0, 1.6f, 0);
        public Action<Creature> OnDead { get; set; }
        public List<Buff> Buffs { get; set; } = new();
        public BaseAI AI { get; set; }
        public Mover Mover { get; set; }
        public HealthBar HealthBar { get; set; }
        public Animator Animator { get; set; }
        public GameBase.Move.CircleCollider Collider { get; set; }
        public bool Alive { get; internal protected set; }
        public GameObject Obj { get; set; }
        public string Key { get; set; }
        public int ID => id;
        public Vector3 Dir => Obj.transform.forward;

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + HealthBarOffset;

        float IHealthBarOwner.CurrHP => _modifyables["currHP"].Value;

        float IHealthBarOwner.MaxHP => _modifyables["maxHP"].Value;

        public Vector3 HandPosition => Obj.transform.position + new Vector3(0, 1, 0);

        public Vector3 Position
        {
            get => Obj.transform.position;
            set => Obj.transform.position = value;
        }

        float IMover.MoveSpeed => _modifyables["moveSpeed"].Value;

        float IMover.RotateSpeed => _modifyables["rotateSpeed"].Value;

        public void AddBuff(string name, float duration = -1)
        {
            BuffFactory.Instance.Get(name).AddTo(this, duration);
        }

        public void AddAI(AIType type)
        {
            AIFactory.Get(type).AddTo(this);
        }

       void IBuffOwner.OnGetBuff(Buff buff)
        {
            Buffs.Add(buff);
        }

        void IBuffOwner.OnRemoveBuff(Buff buff)
        {
            Buffs.Remove(buff);
        }

        public void AddCollider()
        {
            Collider = CollideSys.Instance.NewEntity<CircleCollider>();
            Collider.Owner = Obj.transform;
            Collider.r = Radius;
        }

        public void Interrupt()
        {
            Mover.Interrupt();
            for (int i = 0; i < Spells.Size; i++)
            {
                if (Spells.HasItem(i))
                {
                    Spells[i].Interrupt();
                }
            }
        }
    }
}
