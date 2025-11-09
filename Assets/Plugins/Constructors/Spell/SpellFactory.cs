using GameBase.EntitySystem;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace Constructor.Spells
{
    [GenTemplate(null, "this is a Spell Template")]
    public class SpellFactory : YamlFactory<SpellData, Spell, SpellFactory>
    {
        protected override string YamlFolder => $"{Application.streamingAssetsPath}/ConstructorData/Spell/Main";

        protected override void OnInitYamlData(SpellData data)
        {
            if (data == null || data.actionData == null || data.actionData.projectile == null)
            {
                return;
            }
            if (!SuperEnum.TryParse(data.actionData.projectile.tag, out data.actionData.projectile.tagEnum))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid enum string: {data.tag}");
            }
        }

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

            spell.indicatorType = data.indicator.type;
            spell.radius = data.indicator.radius;
            spell.length = data.indicator.length;

            return spell;
        }
    }
}
