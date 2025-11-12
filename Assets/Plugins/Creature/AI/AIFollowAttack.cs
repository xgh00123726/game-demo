using GameBase.Creatures;
using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;
using System;
using UnityEngine;

namespace GameBase.AI
{
    public class AIFollowAttack : BehaviorTreeAI
    {
        public Creature target;
        public Mover mover;

        public Action OnFollowArrive;

        public bool findTargetEnable = true;
        public float findTargetDisableTime = 1f;
        public bool arriveEnable = true;
        public float arriveDisableTime = 1f;

        public float followMoment = 0f;
        public float missMoment = 0f;

        public float arriveDis = 0.5f;  // 距离低于0.5的时候算到达
        public float refollowDelay = 3f; // 重新寻找目标的延迟
        public float missDis = 5f;    // 目标丢失距离
        public float missTime = 15f;  // 目标丢失时间
        public float stopAfterMissTime = 3f; // 目标丢失后还继续移动的时间

        public AIFollowAttack()
        {
            _builder.Selector()
                        .Timer(10)
                            .FFF(FindTarget)
                        .Back()

                        .FFF(Follow)

                        .Sequence()
                            .IF(HasTarget)
                            .FFF(RecordFollowMoment)
                        .Back()

                        .Timer(12)
                        .Sequence() // 第一优先级：如果追击完成，则在随机的0-1s内发起追击技能，且追击进入cd
                            .And()
                                .IF(IsArriveEnable)
                                .IF(IsFollowArrive)
                            .Back()
                            .FF(FollowArrive)
                            .FF(DisableArrive)
                            .FF(DelayEnableArrive)
                            .FF(Stop)
                            .FF(DisableFindTarget)
                            .FF(DelayEnableFindTarget)
                            .FF(LoseTarget)
                        .Back()
                        .Back()

                        .Sequence() // 第二优先级，如果追击超过距离或者时间，就丢失敌人
                            .IF(HasTarget)
                            .Or()
                                .IF(IsTimeMiss)
                                .IF(IsDistanceMiss)
                            .Back()
                            .FF(LoseTarget)
                            .FF(RecordMissMoment)
                        .Back()
                         
                        .Sequence() // 第三优先级，如果丢失敌人超过一定时间，就直接停下
                            .And()
                                .NIF(HasTarget)
                                .IF(IsMissToLong)
                            .Back()
                            .FF(Stop)
                        .Back()

            .Back().Check().TickRate(5);
        }

        protected override void OnAddTo(Creature c)
        {
            mover = c.mover;
            if (!c.HasPossibleAttr("FollowRange"))
            {
                c.AddPossibleAttr("FollowRange");
                c.SetPossibleAttr("FollowRange", missDis);
            }
        }

        protected internal override void Update()
        {
            missDis = owner.GetPossibleAttr("FollowRange");
            base.Update();
        }

        private bool IsFollowArrive()
        {
            if (target == null)
            {
                return false;
            }

            return GMath.GameDistance(target.Position, mover.owner.Position) <= arriveDis + owner.radius + target.radius;
        }

        private bool IsDistanceMiss()
        {
            if (target == null)
            {
                return false;
            }
            var ret = GMath.GameDistance(target.Position, mover.owner.Position) >= missDis;
            return ret;
        }

        private bool IsTimeMiss()
        {
            var ret = Time.time > followMoment + missTime;
            return ret;
        }

        public bool IsMissToLong()
        {
            return Time.time > missMoment + stopAfterMissTime;
        }


        private bool HasTarget()
        {
            return target != null;
        }

        private bool IsArriveEnable()
        {
            return arriveEnable;
        }

        private void RecordFollowMoment()
        {
            followMoment = Time.time;
        }

        public void EnableArrive()
        {
            arriveEnable = true;
        }

        public void DisableArrive()
        {
            arriveEnable = false;
        }

        public void DelayEnableArrive()
        {
            Timer.AddTask(arriveDisableTime, EnableArrive);
        }

        public void EnableFindTarget()
        {
            findTargetEnable = true;
        }

        public void DelayEnableFindTarget()
        {
            Timer.AddTask(findTargetDisableTime, EnableFindTarget);
        }

        public void DisableFindTarget()
        {
            findTargetEnable = false;
        }

        private void FindTarget()
        {
            if (!findTargetEnable)
            {
                return;
            }
            if (target == null)
            {
                target = CreatureSys.Instance.NearestEntity(mover.owner.Position, Camp.Typedef.Player, missDis);
            }
        }

        private void RecordMissMoment()
        {
            missMoment = Time.time;
        }

        private void LoseTarget()
        {
            target = null;
        }

        private void FollowArrive()
        {
            OnFollowArrive?.Invoke();
        }

        public void Stop()
        {
            mover.Interrupt();
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