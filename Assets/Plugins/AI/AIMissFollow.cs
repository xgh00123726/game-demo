using GameBase.Creatures;
using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.AI
{
    public class AIMissFollow : BehaviorTreeAI
    {
        private bool _followEnable = false;
        private float _enableTime = 0f;

        public Creature target;
        public Mover mover;
        public Rotater rotater;

        public float arriveDis = 0.2f;
        public float refollowDelay = 2f;
        public float missDis = 2f;
        public float missTime = 5f;

        public AIMissFollow()
        {
            _builder.Selector()
                        .Sequence() // 无优先级，寻找目标
                            .FFF(FindTarget)
                        .Back()

                        .Sequence() // 第一优先级：如果追击完成，则追击进入cd，并发起追击技能
                            .IF(IsFollowArrive)
                            .FF(OnFollowArrive)
                            .FF(DisableFollow)
                            .FF(DelayRefollow)
                        .Back()

                        .Sequence() // 第二优先级，如果追击超过距离或者时间，就放弃追击，且追击进入cd
                            .Selector()
                                .IF(IsMissTime)
                                .IF(IsMissDistance)
                            .Back()
                            .FF(DisableFollow)
                            .FF(DelayRefollow)
                        .Back()

                        .Sequence() // 第三优先级，如果没有追击完成，且追击cd ok，重新追击
                            .Inverter()
                                .IF(IsFollowArrive)
                            .Back()
                            .IF(IsFollowEnable)
                            .FF(Follow)
                        .Back()



            .End().TickRate(100);
        }

        public override void AddTo(Creature c)
        {
            mover = c.Mover;
            rotater = c.Rotater;
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

        private bool IsMissDistance()
        {
            if (target == null)
            {
                return false;
            }

            return GMath.GameDistance(target.Position, mover.owner.Position) >= missDis;
        }

        private bool IsMissTime()
        {
            return Time.time > _enableTime + missTime;
        }

        private void FindTarget()
        {
            if (target == null)
            {
                target = CreatureSys.Instance.NearestEntity(mover.owner.Position, CreatureTag.Player);
            }
        }

        private void OnFollowArrive()
        {

        }

        private void EnableFollow()
        {
            _enableTime = Time.time;
            _followEnable = true;
        }

        private void DelayRefollow()
        {
            Timer.AddTask(refollowDelay, EnableFollow);
        }

        private void DisableFollow()
        {
            _followEnable = false;
        }

        private void Follow()
        {
            if (target == null)
            {
                return;
            }

            mover.MoveTo(target.Position);
        }
    }
}
