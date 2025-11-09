using GameBase.Creatures;
using GameBase.Flyings;
using GameBase.Triggers;

namespace GameBase.Projectiles
{
    public enum SearchTargetStyle
    {
        TraceTarget,
        BaseFlying,
    };

    public enum Tag : uint
    {
        None = 0,
        DestroyOnEffectMaxTimes = 1 << 0,       // 到达最大次数后销毁
        TrigOnlyWhenHitMainTarget = 1 << 1,     // 射弹只有击中主目标后才触发
    }

    public class Projectile
    {
        public Flying flying;
        public Trigger trigger;
        public Creature target;

        public SearchTargetStyle searchTargetStyle;
        public Tag tag;
        public float arriveDis;
    }
}
