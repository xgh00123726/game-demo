using System;
using UnityEngine;

namespace GameBase.Animations
{
    public class PlayerAnimController2
    {
        private Animator _animator;
        private AnimationClip[] _clips;
        private Func<bool> _IsMoving;
        private Func<bool> _IsIdle;

        private SimpleAnimClipController idleController;
        private SimpleAnimClipController moveController;
        private SimpleAnimClipController boringController;

        public PlayerAnimController2(IPlayerAnimable player)
        {
            _animator = player.Animator;
            _clips = _animator.runtimeAnimatorController.animationClips;
            _IsMoving = player.IsMoving;
            _IsIdle = player.IsIdle;

            idleController = new SimpleAnimClipController(_animator, _IsIdle, "HumanIdle");
            moveController = new SimpleAnimClipController(_animator, _IsMoving, "HumanRun");
            boringController = new SimpleAnimClipController(_animator, () =>
            {
                return _IsIdle() && (idleController.stateDuration % 20) > 10;
            }, "HumanBoring");
        }

        public void Update()
        {
            idleController.Update();
            moveController.Update();
            boringController.Update();
        }
    }
}


