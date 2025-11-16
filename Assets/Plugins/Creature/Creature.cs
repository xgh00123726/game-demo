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

        public float radius = 0.3f;
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
        public string Key { get; set; }
        public int ID => id;
        public Vector3 Dir => obj.transform.forward;

        Vector3 IHealthBarOwner.HealthBarPosition => obj.transform.position + healthBarOffset;

        Vector3 ITriggerTarget.Position => obj.transform.position;

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

        float IMover.RotateSpeed => modifyables["rotateSpeed"].Value;

        GameObject IMover.Obj => obj;

        float IMover.Radius => radius;

        Move.CircleCollider IMover.Collider => collider;

        


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

        public void Interrupt()
        {
            mover.Interrupt();
            for (int i = 0; i < spells.Size; i++)
            {
                if (spells.HasItem(i))
                {
                    spells[i].Interrupt();
                }
            }
        }
    }
}
