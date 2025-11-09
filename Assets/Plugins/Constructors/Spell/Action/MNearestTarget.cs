using Constructor.Flyings;
using Constructor.Triggers;
using Constructor.Triggers.Action;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Spells;
using GameBase.Triggers;
using UnityEngine;

namespace Constructor.Spells.Action
{
    /// <summary>
    /// 召唤单体射弹，射弹自动追踪最近的敌人
    /// </summary>
    public class MNearestTarget : ModifyableAction
    {
        protected override Flying GenFlying(Spell spell, in SpellActionModifierData modifyData)
        {
            if (spell.speller is Creature c)
            {
                float attackRange = c.modifyables["attackRange"].Value;

                var center = new Vector3(c.Position.x, 0, c.Position.z);
                var target = CommonTargetSet.Instance.NearestTarget(center, attackRange, spell.camp);
                if (target != null)
                {
                    var t = TriggerSys.Instance.NewEntity();
                    t.owner = c;
                    t.target = target;
                    t.hitAudio = data.hitAudio;
                    t.maxeffectTimes = data.maxEffectTimes;
                    t.targetsSet = CommonTargetSet.Instance;

                    var damage = data.damage + c.modifyables["damage"].Value * data.ampFactor;
                    t.action = new Damage(damage);

                    var f = Flyings.FlyingFactory.Instance.GetFromData(data.flying);
                    f.Src = c.HandPosition;
                    f.target = new FixedFlyingTarget()
                    {
                        Position = target.Position,
                    };
                    f.OnHit = t.Trig;
                    f.arriveDis += t.target.Radius;

                    return f;
                }
            }

            return null;
        }
    }
}
