using GameBase.Creatures;
using UnityEngine;

namespace GameBase.Animations
{
    public class ZombieAnimController : AnimController
    {
        private Animator _animator;

        private SimpleAnimClipController idleController;
        private SimpleAnimClipController moveController;
        private SimpleAnimClipController attackController;

        public override void Update()
        {
            moveController.Update();
            attackController.Update();
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

        private bool IsAttack()
        {
            return false;
        }

        public override void OnAddTo(Creature creature)
        {
            _animator = creature.Animator;

            idleController = new SimpleAnimClipController(_animator, IsIdle, "Idle");
            moveController = new SimpleAnimClipController(_animator, IsMoving, "Run");
            attackController = new SimpleAnimClipController(_animator, IsAttack, "Attack");
        }
    }
}
