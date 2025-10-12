using GameBase.Modify;
using GameBase.Triggers;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;

namespace Constructor.Triggers.Action
{
    public struct DamageData
    {
        public float value;
    }

    public class Damage : ITriggerAction
    {
        public DamageData data;

        public Damage(float value = 0)
        {
            data.value = value;
        }

        void ITriggerAction.Effect(Trigger e, ITriggerTarget target)
        {
            if (target is IModifieder mTarget)
            {
                var modifyer = ModifyerSys.Instance.NewEntity();
                modifyer.type = ModifyType.Once | ModifyType.Forever;
                modifyer.value = -data.value;
                modifyer.OnModify += () =>
                {
                    var text = TextSys.Instance.NewEntity((e) => { e.showPosition = target.Center; });
                    text.showPosition = target.Center;
                    text.Value = data.value.ToString();
                    text.Color = Color.white;
                };

                modifyer.AddTo(mTarget.Modifyables["currHP"]);
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
