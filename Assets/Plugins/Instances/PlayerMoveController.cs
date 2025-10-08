using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Tools;
using System;

namespace Instance
{
    public class PlayerMoveController : SingletonInstance<PlayerMoveController>
    {
        private static Creature _target;

        public static Action OnMoveInput;

        protected override void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.MoveTo, "mover"))
            {
                _target.mover.MoveTo(CameraSys.MouseHitPosition);
                _target.rotater.LookAt(CameraSys.MouseHitPosition);
                MoveIndicator.Show(CameraSys.MouseHitPosition);

                OnMoveInput?.Invoke();
            }

            if (Inputs.GetKeyDown(KeyFunction.Stop, "mover"))
            {
                _target.mover.Stop();
                _target.Dir = _target.Dir;
            }
        }

        public static void SetTarget(Creature target)
        {
            _target = target;
        }
    }
}
