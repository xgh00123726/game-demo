using GameBase.Creatures;
using GameBase.Flyings;
using GameBase.Triggers;
using UnityEngine;

namespace GameBase.Projectiles
{
    public enum Tag : uint
    {
        None = 0,
        DestroyOnEffectMaxTimes = 1 << 0,       // 到达最大次数后销毁
        TrigOnlyWhenHitMainTarget = 1 << 1,     // 射弹只有击中主目标后才触发
        DestroyOnFlyingEnd = 1 << 2,            // 射弹飞行结束后会自行销毁
        AutoFindPossibleTarget = 1 << 3,        // 射弹会尽可能寻找目标（如果没有目标）
    }

    public class Projectile
    {
        public Flying flying;
        public Trigger trigger;

        public float findTargetRange;
        public float damage;
        public float ampFactor;
        public Color damageTextColor;
        public string damageTextPrefabName;

        public Tag tag;
        public float arriveDis;

        internal bool alive;
        internal Creature target;
        internal Creature owner;

        public bool Alive => alive;
        public bool IsDestroyOnEffectMaxTimes() => (tag & Tag.DestroyOnEffectMaxTimes) != 0;
        public bool IsTrigOnlyWhenHitMainTarget() => (tag & Tag.TrigOnlyWhenHitMainTarget) != 0;
        public bool IsDestroyOnFlyingEnd() => (tag & Tag.DestroyOnFlyingEnd) != 0;
        public bool IsAutoFindPossibleTarget() => (tag & Tag.AutoFindPossibleTarget) != 0;

        public Creature Target
        {
            get => target;
            set
            {
                target = value;
                if (target == null)
                {
                    return;
                }
                if (trigger.targetCamp == null)
                {
                    trigger.targetCamp = target.camp;
                }
                else
                {
                    trigger.targetCamp = trigger.targetCamp.Or(target.camp);
                }
            }
        }

        public Creature Owner
        {
            get => owner;
            set
            {
                owner = value;
                trigger.owner = value;
                flying.Src = owner.HandPosition;
            }
        }
    }
}
