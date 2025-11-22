using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Triggers;
using GameBase.UI;
using UnityEngine;

namespace GameBase.Projectiles
{
    public class ProjectileAction : ITriggerAction
    {
        public float damage;
        public Color color;
        public string prefabName;

        void ITriggerAction.Effect(Trigger e, ITriggerTarget target)
        {
            if (target is Creature c)
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
        }
    }
}
