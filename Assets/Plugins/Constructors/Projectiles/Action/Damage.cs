using GameBase.Modify;
using GameBase.Projectiles;
using GameBase.Tools;
using GameBase.UI;

namespace Constructor.Projectiles.Action
{
    public struct DamageData
    {
        public int value;
    }

    public class Damage : IProjectileAction
    {
        public DamageData data;
        void IProjectileAction.Effect(Projectile e, IProjectileTarget target)
        {
            if (target is IModifyOwner<float> mTarget)
            {
                var modifyer = ModifyerSys<float>.Instance.NewEntity();
                modifyer.type = ModifyType.Once | ModifyType.Forever;
                modifyer.ModifyFunc += ConvientModifyerFunc.FloatFixedValue(-data.value);
                modifyer.OnModify += () =>
                {
                    var text = TextSys.Instance.NewEntity();
                    text.showPosition = target.Center;
                    text.value = data.value.ToString();
                };

                mTarget.Modifyables.AddModify("currHP", modifyer);
            }
        }
    }

    public class DamageCon
    {
        public static Damage Get(int value)
        {
            var e = new Damage();
            e.data.value = value;
            return e;
        }
    }
}
