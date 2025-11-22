using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Triggers;
using GameBase.UI;
using UnityEngine;

namespace Constructor.Triggers
{
    public enum TriggerActionTag
    {
        HasDamage = 1 << 0,
        AddBuff = 1 << 1,
    }
    public class TriggerAction : ITriggerAction
    {
        public TriggerActionTag tag;
        public float damage;
        public Color color;
        public float buffDuration;
        public string prefabName;
        public string buffName;

        void ITriggerAction.Effect(Trigger e, ITriggerTarget target)
        {
            if (target is not Creature c)
            {
                return;
            }

            if ((tag & TriggerActionTag.HasDamage) != 0)
            {
                var modifyer = ModifyerSys.Instance.NewEntity();
                modifyer.Type = ModifyType.Once | ModifyType.Forever;
                modifyer.Value = -damage;
                modifyer.OnModify += () =>
                {
                    var text = TextSys.Instance.NewEntity(prefabName);
                    text.showPosition = target.Position;
                    text.Value = damage.ToString();
                    text.Color = color;
                };

                modifyer.AddTo(c.Modifyables["currHP"]);
            }

            if ((tag & TriggerActionTag.AddBuff) != 0)
            {
                var buff = BuffFactory.Instance.Get(buffName);
                buff.AddTo(c, buffDuration);
            }
        }
    }
}
