using GameBase.Modify;
using GameBase.Triggers;
using GameBase.Tools;
using GameBase.UI;

namespace Constructor.Triggers.Action
{
    public struct DamageData
    {
        public int value;
    }

    public class Damage : ITriggerAction
    {
        public DamageData data;
        void ITriggerAction.Effect(Trigger e, ITriggerTarget target)
        {
            if (target is IModifieder mTarget)
            {
                var modifyer = ModifyerSys.Instance.NewEntity();
                modifyer.type = ModifyType.Once | ModifyType.Forever;
                modifyer.value = -data.value;
                modifyer.OnModify += () =>
                {
                    var text = TextSys.Instance.NewEntity();
                    text.showPosition = target.Center;
                    text.value = data.value.ToString();
                };

                mTarget.Modifyables.ModifySet("currHP", modifyer);
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
