using GameBase.Creatures;
using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;

namespace GameBase.AI
{
    public class AIFollow : BehaviorTreeAI
    {
        private bool _followEnable = true;

        public Creature target;
        public Mover mover;
        public Rotater rotater;

        public float arriveDis = 0.2f;

        public AIFollow()
        {
            _builder.Selector()
                        .Sequence()
                            .FFF(FindTarget)
                        .Back()

                        .Sequence()
                            .IF(IsFollowArrive)
                            .FF(OnFollowArrive)
                            .FF(DisableFollow)
                        .Back()

                        .Sequence()
                            .Inverter()
                                .IF(IsFollowArrive)
                            .Back()
                            .IF(IsFollowEnable)
                            .FF(Follow)
                        .Back()
            .Check().TickRate(1);
        }

        protected override void OnAddTo(Creature c)
        {
            mover = c.mover;
            rotater = c.rotater;
        }

        private bool IsFollowArrive()
        {
            if (target == null)
            {
                return false;
            }

            return GMath.GameDistance(target.Position, mover.owner.Position) <= arriveDis;
        }

        private bool IsFollowEnable()
        {
            return _followEnable;
        }

        private void OnFollowArrive()
        {
            
        }

        private void FindTarget()
        {
            if (target == null)
            {
                target = CreatureSys.Instance.NearestEntity(mover.owner.Position, CreatureTag.Player);
            }
        }

        private void EnableFollow()
        {
            _followEnable = true;
        }

        private void DisableFollow()
        {
            _followEnable = false;
            Timer.AddTask(3, EnableFollow);
        }

        private void Follow()
        {
            if (target == null)
            {
                return;
            }

            mover.MoveTo(target.Obj.transform.position);
            rotater.LookAt(target.Obj.transform.position);
        }
    }
}
