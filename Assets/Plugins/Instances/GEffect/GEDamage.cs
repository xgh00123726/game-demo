using Combines.Projectiles;
using GameBase.GEffects;
using GameBase.Modify;
using GameBase.Tools;

namespace Instance.GEffects
{
    public class GEDamage : GEffect<IProjectileOwner, IProjectileTarget>
    {
        public int value;
        private void Effect(IProjectileOwner owner, IProjectileTarget target)
        {
            if (target is IModifyOwner<float> mTarget)
            {
                var modifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                modifyer.ModifyFunc = ConvientModifyerFunc.FloatFixedValue(value);

                mTarget.Modifyables.AddModify("currHP", modifyer);
            }
        }

        public override void AfterGet()
        {
            base.AfterGet();
            EffectAction = Effect;
        }

        public override void BeforeRelease()
        {
            value = 0;
            base.BeforeRelease();
        }

        public static GEDamage New(int value)
        {
            var damage = GEffectSys<IProjectileOwner, IProjectileTarget>.Instance.NewEntity<GEDamage>();
            damage.value = value;
            return damage;
        }
    }
}
