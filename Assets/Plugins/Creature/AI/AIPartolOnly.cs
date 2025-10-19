using GameBase.Creatures;
using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.AI
{
    public class AIPartolOnly : BehaviorTreeAI
    {
        private int _partolDest = 1; // 1£ºÑ²ÂßÈ¥p1£¬2£ºÑ²ÂßÈ¥p2

        public Mover mover;
        public float randomDisMin = 1f;
        public float randomDisMax = 5f;
        public Vector3 partolP1;
        public Vector3 partolP2;

        public AIPartolOnly()
        {
            _builder.Selector()
                        .Sequence()
                            .IF(IsMoveArriveP1)
                            .FF(MoveToP2)
                        .Back()
                        .Sequence()
                            .IF(IsMoveArriveP2)
                            .FF(MoveToP1)
                        .Back()
                        .Sequence()
                            .IF(IsIdle)
                            .FF(MoveToP2)
                        .Back()
            .Check().TickRate(100);
        }

        private bool IsMoveArriveP1()
        {
            return _partolDest == 1 && mover.IsArrive;
        }
        private bool IsMoveArriveP2()
        {
            return _partolDest == 2 && mover.IsArrive;
        }
        private void MoveToP1()
        {
            _partolDest = 1;
            mover.MoveTo(partolP1);
        }

        private void MoveToP2()
        {
            _partolDest = 2;
            mover.MoveTo(partolP2);
        }

        private bool IsIdle()
        {
            return !mover.IsMoving;
        }

        protected override void OnAddTo(Creature c)
        {
            mover = c.mover;
            partolP1 = c.Position;
            var delta = GMath.RollRandomDir(Random.Range(randomDisMin, randomDisMax));
            delta.y = 0;
            partolP2 = c.Position + delta;
        }
    }
}
