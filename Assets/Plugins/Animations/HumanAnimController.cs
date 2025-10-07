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

        public bool boringEnable = false;

        public override void Update()
        {
            idleController.Update();
            moveController.Update();
            boringController.Update();
        }

        private bool IsMoving()
        {
            return owner.mover.IsMoving;
        }

        private bool IsIdle()
        {
            return !owner.mover.IsMoving;
        }

        public override void OnAddTo(Creature creature)
        {
            _animator = creature.animator;

            idleController = new SimpleAnimClipController(_animator, IsIdle, "HumanIdle");
            moveController = new SimpleAnimClipController(_animator, IsMoving, "HumanRun");
            boringController = new SimpleAnimClipController(_animator, () =>
            {
                return boringEnable && IsIdle() && (idleController.stateDuration % 20) > 10;
            }, "HumanBoring");
        }
    }
}


