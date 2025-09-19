using GameBase.Creatures;
using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.AI
{
    public class AIFollow : BehaviorTreeAI
    {
        public GameObject target;
        public Mover mover;
        public Rotater rotater;
        public float arriveDis = 0.2f;

        public AIFollow()
        {
            _builder.Selector()
                        .Sequence()
                            .IF(FollowArrive)
                            .FF(OnFollowArrive)
                        .Back()
                        .Selector()
                            .IF(FollowArrive)
                            .FF(Follow)
                        .Back()
            .End().TickRate(10);
        }

        public override void AddTo(Creature c)
        {
            mover = c.Mover;
            rotater = c.Rotater;
        }

        private bool FollowArrive()
        {
            return GMath.GameDistance(target.transform.position, mover.owner.Position) <= arriveDis;
        }

        private void OnFollowArrive()
        {

        }

        private void Follow()
        {
            mover.MoveTo(target.transform.position);
        }
    }
}
