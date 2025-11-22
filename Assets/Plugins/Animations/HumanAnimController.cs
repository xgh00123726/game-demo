using GameBase.Creatures;
using System;
using UnityEngine;

namespace GameBase.Animations
{
    public class HumanAnimController : AnimController
    {
        private Animator _animator;

        private SimpleAnimClipController idleController;
        private SimpleAnimClipController moveController;
        private SimpleAnimClipController boringController;

        public bool BoringEnable { get; set; } = false;

        public override void Update()
        {
            moveController.Update();
            boringController.Update();
            idleController.Update();
        }

        private bool IsMoving()
        {
            return owner.Mover.IsMoving;
        }

        private bool IsIdle()
        {
            return !owner.Mover.IsMoving;
        }

        public override void OnAddTo(Creature creature)
        {
            _animator = creature.Animator;

            idleController = new SimpleAnimClipController(_animator, IsIdle, "Idle");
            moveController = new SimpleAnimClipController(_animator, IsMoving, "Run");
            boringController = new SimpleAnimClipController(_animator, () =>
            {
                return BoringEnable && IsIdle() && (idleController.stateDuration % 20) > 10;
            }, "Boring");
        }
    }
}


