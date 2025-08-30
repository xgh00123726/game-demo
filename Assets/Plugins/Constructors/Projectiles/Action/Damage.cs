using GameBase.Modify;
using GameBase.Projectiles;
using GameBase.UI;

namespace Constructor.Projectiles.Action
{
    public struct DamageData
    {
        public int value;
    }

    public class DamageAction : IProjectileAction
    {
        public DamageData data;
        void IProjectileAction.Action(Projectile e)
        {
            if (e.target is IModifyOwner<float> mTarget)
            {
                var modifyer = ModifyerSys<float>.Instance.NewEntity();
                modifyer.type = ModifyType.Once | ModifyType.Forever;
                modifyer.ModifyFunc += ConvientModifyerFunc.FloatFixedValue(-data.value);
                modifyer.OnModify += () =>
                {
                    var text = TextSys.Instance.NewEntity();
                    text.showPosition = e.target.Center;
                    text.value = data.value.ToString();
                };

                mTarget.Modifyables.AddModify("currHP", modifyer);
            }
        }
    }

    public class Damage
    {
        public static DamageAction Get(int value)
        {
            var e = new DamageAction();
            e.data.value = value;
            return e;
        }
    }
}
