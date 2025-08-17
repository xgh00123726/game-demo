using Combines.Projectiles;
using GameBase.Animations;
using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Move;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.UI;
using System;
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
        IMover,
        IRotater,
        IPlayerAnimable
    {
        public GameCreature()
        {
            AfterInstantiateObj += AfterInstantitate;
        }
        private void AfterInstantitate()
        {
            _animator = Obj.GetComponent<Animator>();
        }

        protected ModifyableContainer<float> _modifyableContainer = new();
        protected SpellContainer _spellContainer = new();
        protected BuffContainer _buffContainer = new();
        protected Animator _animator;

        public ModifyableContainer<float> ModifyableContainer => _modifyableContainer;
        public BuffContainer BuffContainer => _buffContainer;

        public override bool ReleaseTrigger => _modifyableContainer["currHP"].Value <= 0f;

        Vector3 IHealthBarOwner.HealthBarPosition => Obj.transform.position + new Vector3(0, 1, 1);

        Vector3 IProjectileTarget.Center => Obj.transform.position;

        float IProjectileTarget.Radius => radius;

        float IHealthBarOwner.CurrHP => _modifyableContainer["currHP"].Value;

        float IHealthBarOwner.MaxHP => _modifyableContainer["maxHP"].Value;

        bool IHealthBarOwner.ALive => Alive;

        ModifyableContainer<float> IModifyOwner<float>.Modifyables => _modifyableContainer;

        Vector3 IProjectileOwner.HandPosition => Obj.transform.position + new Vector3(0, 1, 0);

        float ISpeller.CoolingAccelerate => _modifyableContainer["coolingAccelerate"].Value;

        Vector3 ISpeller.Position => Obj.transform.position;

        BuffContainer IBuffOwner.Buffs => _buffContainer;

        float IMover.Speed => _modifyableContainer["moveSpeed"].Value;

        GameObject IMover.GO => Obj;

        float IRotater.Speed => _modifyableContainer["rotateSpeed"].Value;

        GameObject IRotater.GO => Obj;

        public bool IsMoving { get; set; }
        public bool IsRotating { get; set; }

        Animator IPlayerAnimable.Animator => _animator;

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
    }
}
