using GameBase.Creatures;
using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.AI
{
    public class AIFollowAttack : BehaviorTreeAI
    {
        private float _followEnableTime = 0f;

        public Creature target;
        public Mover mover;
        public Rotater rotater;

        public float arriveDis = 0.5f;
        public float refollowDelay = 3f;
        public float missDis = 5f;
        public float missTime = 25f;

        public AIFollowAttack()
        {
            _builder.Selector()
                        .FFF(Follow) // 更新目标

                        .Timer(180)
                            .Selector() // 无优先级，寻找目标
                                .FFF(FindTarget)
                                .FFF(Follow)
                            .Back()
                        .Back()

                        .Sequence() // 第一优先级：如果追击完成，则追击进入cd，并发起追击技能
                            .IF(IsFollowArrive)
                            .FF(OnFollowArrive)
                        .Back()

                        .Sequence() // 第二优先级，如果追击超过距离或者时间，就丢失敌人
                            .Selector()
                                .IF(IsMissTime)
                                .IF(IsMissDistance)
                            .Back()
                            .FF(LoseTarget)
                        .Back()

            .End().TickRate(5);
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

        private bool IsMissDistance()
        {
            if (target == null)
            {
                return false;
            }
            var ret = GMath.GameDistance(target.Position, mover.owner.Position) >= missDis;
            return ret;
        }

        private bool IsMissTime()
        {
            var ret = Time.time > _followEnableTime + missTime;
            return ret;
        }

        private void FindTarget()
        {
            if (target == null)
            {
                target = CreatureSys.Instance.NearestEntity(mover.owner.Position, Tag.Player, missDis);
                if (target != null)
                {
                    _followEnableTime = Time.time;
                }
            }
        }

        private void LoseTarget()
        {
            target = null;
        }

        private void OnFollowArrive()
        {

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