using Constructor.Flyings;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Math;
using GameBase.Resources;
using GameBase.Spells;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    /// <summary>
    /// ’ŸªΩ∑…––πÃ∂®æ‡¿Îµƒ…‰µØ£¨…‰µØŒ™AOE
    /// </summary>
    public class MAreaFixedDis : ModifyableAction
    {
        protected override Flying GenFlying(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                var f = Flyings.FlyingFactory.Instance.GetFromData(data.flying);
                f.Src = c.Position;
                var dir = (spell.castPosition - c.Position).normalized;
                f.target = new FixedFlyingTarget()
                {
                    Position = f.Src + dir * data.distance,
                };

                var t = TriggerSys.Instance.NewEntity();
                t.hasWhite = true;
                t.maxeffectTimes = data.maxEffectTimes;
                t.shape = new GMath.Circle(data.radius);
                t.owner = c;
                t.attach = f;
                t.trigStyle = TrigStyle.Always;
                t.camp = (GameBase.Triggers.CampType)spell.camp;
                t.hitAudio = data.hitAudio;
                t.targetsSet = CommonTargetSet.Instance;

                var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                t.action = new Damage(damage);

                return f;
            }

            return null;
        }
    }
}
