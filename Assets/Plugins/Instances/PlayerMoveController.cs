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
                _target.Mover.MoveTo(CameraSys.MouseHitPosition);
                _target.Dir = CameraSys.MouseHitPosition - _target.Obj.transform.position;
                MoveIndicator.Show(CameraSys.MouseHitPosition);

                OnMoveInput?.Invoke();
            }
        }

        public static void SetTarget(Creature target)
        {
            _target = target;
        }
    }
}
