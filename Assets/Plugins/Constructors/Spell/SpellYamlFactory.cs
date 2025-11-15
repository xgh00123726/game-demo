using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace Constructor.Spells
{
    [GenTemplate(null, "this is a Spell Template")]
    public class SpellYamlFactory : YamlFactory<SpellData, Spell, SpellYamlFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/ConstructorData/Spell/Main";

        protected override Spell GetEntity(SpellData data)
        {
            var spell = SpellSys.Instance.NewEntity();
            spell.minCastAngle = data.minCastAngle;
            spell.textureName = data.textureName;
            spell.cooldown = data.cooldown;
            spell.tag = data.tag;

            spell.targetCampSet = new CampSet()
            {
                include = data.camp.include,
                exclude = data.camp.exclude,
            };
            
            spell.action = SpellActionFactory.Instance.Get(data.actionName);

            spell.indicatorType = data.indicator.type;
            spell.radius = data.indicator.radius;
            spell.length = data.indicator.length;

            return spell;
        }
    }
}
