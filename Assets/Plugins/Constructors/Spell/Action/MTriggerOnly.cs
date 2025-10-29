using Constructor.Spells.Interactive;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Resources;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    public struct MTriggerOnlyData
    {
        public int effectTimes;
        public TargetSetType targetSetType;
        public TriggerType triggerType;
        public int triggerID;
        public string effectName;
        public int slotNum;
        public float damage;
        public float ampFactor;
        public float delay;
    }

    /// <summary>
    /// 只有一个触发器
    /// </summary>
    public class MTriggerOnly : ModifyableAction
    {
        public MTriggerOnlyData data;

        protected override bool CastAction(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c
                && spell.interactive is DotExternalSet interacitve)
            {
                var t = TriggerFactory.Instance.Get(data.triggerType, data.triggerID);
                t.trigStyle = TrigStyle.Once;
                t.delay = data.delay;
                t.trigPeriod = 0;
                t.hasWhite = true;
                t.attach = c;
                t.targetsSet = TargetSetFactorary.Get(data.targetSetType);
                t.maxeffectTimes = data.effectTimes;
                t.owner = c;
                var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                t.action = new Damage(damage);


                Vector3 position = c.Position;
                float size = t.shape.Size;
                Vector3 scale = new Vector3(size, 1, size);
                Vector3 dir = interacitve.position - c.Position;

                EffectSys.Instance.PlayAtPSD(data.effectName, c.Position, scale, dir);

                t.OnTrig += () =>
                {
                    t.shape.Center = new Vector2(c.Position.x, c.Position.z);
                    var dir = interacitve.position - c.Position;
                    t.shape.Dir = new Vector2(dir.x, dir.z);
                };

                return true;
            }

            return false;
        }
    }

    public class MTriggerOnlyCon : SealedConstructor<MTriggerOnlyData, MTriggerOnly, MTriggerOnlyCon>
    {
        protected override string RelativePath => "Spell/Action/MTriggerOnly.csv";

        protected override MTriggerOnly GetFromData(in MTriggerOnlyData data)
        {
            var action = new MTriggerOnly()
            {
                Size = data.slotNum
            };
            action.data = data;
            return action;
        }
    }
}
