using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.GCamera;
using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace Instance
{
    public class PlayerMoveController : SingletonInstance<PlayerMoveController>
    {
        private static LinkedList<Creature> _targets = new();

        public static Action OnMoveInput;

        protected override void Update()
        {
            if (Inputs.GetKeyDown(KeyFunction.MoveTo, "mover"))
            {
                foreach (var c in _targets)
                {
                    c.mover.MoveTo(CameraSys.MouseHitPosition);
                    c.rotater.LookAt(CameraSys.MouseHitPosition);
                    c.ai?.Disable();
                }

                MoveIndicator.Show(CameraSys.MouseHitPosition);

                OnMoveInput?.Invoke();
            }

            if (Inputs.GetKeyDown(KeyFunction.Stop, "mover"))
            {
                foreach (var c in _targets)
                {
                    c.mover.Stop();
                    c.Dir = c.Dir;
                    c.ai?.Disable();
                }
            }
        }

        public static void SetTarget(Creature target)
        {
            _targets.Clear();
            _targets.AddFirst(target);
        }

        public static void ClearTarget()
        {
            _targets.Clear();
        }

        public static void AddTarget(Creature target)
        {
            _targets.AddLast(target);
        }
    }
}
