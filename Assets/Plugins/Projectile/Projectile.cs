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
        internal bool alive;
        internal Creature target;
        internal Creature owner;

        public Flying Flying { get; set; }
        public Trigger Trigger { get; set; }
        public float FindTargetRange { get; set; }
        public float Damage {  get; set; }
        public float AmpFactor { get; set; }
        public Color DamageTextColor { get; set; }
        public string DamageTextPrefabName { get; set; }
        public Tag Tag { get; set; }
        public float ArriveDis {  get; internal set; }
        public bool Alive => alive;
        public bool IsDestroyOnEffectMaxTimes => (Tag & Tag.DestroyOnEffectMaxTimes) != 0;
        public bool IsTrigOnlyWhenHitMainTarget => (Tag & Tag.TrigOnlyWhenHitMainTarget) != 0;
        public bool IsDestroyOnFlyingEnd => (Tag & Tag.DestroyOnFlyingEnd) != 0;
        public bool IsAutoFindPossibleTarget => (Tag & Tag.AutoFindPossibleTarget) != 0;

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
                if (Trigger.TargetCamp == null)
                {
                    Trigger.TargetCamp = target.Camp;
                }
                else
                {
                    Trigger.TargetCamp = Trigger.TargetCamp.Or(target.Camp);
                }
            }
        }

        public Creature Owner
        {
            get => owner;
            set
            {
                owner = value;
                Trigger.Owner = value;
                Flying.Src = owner.HandPosition;
            }
        }
    }
}
