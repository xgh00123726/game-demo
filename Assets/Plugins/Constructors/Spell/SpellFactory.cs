using GameBase.EntitySystem;
using GameBase.Spells;
using UnityEngine;

namespace Constructor.Spells
{
    public class SpellFactory : YamlFactory<SpellData, Spell, SpellFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/ConstructorData/Spell/Main";

        protected override Spell GetEntity(SpellData data)
        {
            var spell = SpellSys.Instance.NewEntity();
            spell.minCastAngle = data.minCastAngle;
            spell.textureName = data.textureName;
            spell.cooldown = data.cooldown;
            spell.tag = data.tag;

            spell.campInclude = data.camp.include;
            spell.campExclude = data.camp.exclude;
            spell.campFixed = data.camp.fixedType;
            spell.action = SpellActionFactory.Instance.GetFromData(data.actionData);

            return spell;
        }
    }
}
