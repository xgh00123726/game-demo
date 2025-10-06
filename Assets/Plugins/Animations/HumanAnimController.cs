using System;
using UnityEngine;

namespace GameBase.Animations
{
    public class HumanAnimController
    {
        private Animator _animator;
        private Func<bool> _IsMoving;
        private Func<bool> _IsIdle;

        private SimpleAnimClipController idleController;
        private SimpleAnimClipController moveController;
        private SimpleAnimClipController boringController;

        public bool boringEnable = false;

        public HumanAnimController(IAnimatable player)
        {
            _animator = player.Animator;
            _IsMoving = player.IsMoving;
            _IsIdle = player.IsIdle;

            idleController = new SimpleAnimClipController(_animator, _IsIdle, "HumanIdle");
            moveController = new SimpleAnimClipController(_animator, _IsMoving, "HumanRun");
            boringController = new SimpleAnimClipController(_animator, () =>
            {
                return boringEnable && _IsIdle() && (idleController.stateDuration % 20) > 10;
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


