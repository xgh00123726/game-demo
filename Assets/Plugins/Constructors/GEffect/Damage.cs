using GameBase.EntitySystem;
using GameBase.GEffects;
using GameBase.Modify;
using GameBase.Projectiles;
using GameBase.UI;
using NReco.Csv;
using System;

namespace Constructor.GEffects
{
    public struct DamageData
    {
        public int value;
    }

    public class DamageEffect : GEffect<IProjectileOwner, IProjectileTarget>
    {
        public int value;
        private void Effect(IProjectileOwner owner, IProjectileTarget target)
        {
            if (target is IModifyOwner<float> mTarget)
            {
                var modifyer = ModifyerSys<float>.Instance.NewEntity<Modifyer<float>>();
                modifyer.type = ModifyType.Once | ModifyType.Forever;
                modifyer.ModifyFunc += ConvientModifyerFunc.FloatFixedValue(-value);
                modifyer.OnModify += () =>
                {
                    var text = TextSys.Instance.NewEntity<FloatText>();
                    text.showPosition = target.Center;
                    text.value = value.ToString();
                };

                mTarget.Modifyables.AddModify("currHP", modifyer);
            }
        }

        public override void AfterGet()
        {
            base.AfterGet();
            EffectAction += Effect;
        }

        public override void BeforeRelease()
        {
            value = 0;
            base.BeforeRelease();
        }
    }

    public class Damage : EntityConstructor<DamageData,
        GEffect<IProjectileOwner, IProjectileTarget>,
        SimpleEntityContainer, GEffectSys<IProjectileOwner, IProjectileTarget>,
        Damage>
    {
        protected override GEffectSys<IProjectileOwner, IProjectileTarget> SysInstance => GEffectSys<IProjectileOwner, IProjectileTarget>.Instance;

        protected override string RelativePath => null;

        protected override void Parse(CsvReader line, ref DamageData data)
        {
            
        }

        public new DamageEffect Get(int value)
        {
            var e = SysInstance.NewEntity<DamageEffect>();
            e.value = value;
            return e;
        }

        protected override void Set(GEffect<IProjectileOwner, IProjectileTarget> e, in DamageData data)
        {
            
        }
    }
}
