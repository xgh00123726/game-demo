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
        protected override string Folder => $"{Application.streamingAssetsPath}/Spell/Main";

        protected override Spell GetEntity(SpellData data)
        {
            var spell = SpellSys.Instance.NewEntity();
            spell.MinCastAngle = data.MinCastAngle;
            spell.TextureName = data.TextureName;
            spell.Cooldown = data.Cooldown;
            spell.Tag = data.Tag;

            spell.TargetCampSet = new CampSet()
            {
                include = data.Camp.Include,
                exclude = data.Camp.Exclude,
            };
            
            spell.Action = SpellActionFactory.Instance.Get(data.ActionName);

            spell.IndicatorType = data.Indicator.Type;
            spell.Radius = data.Indicator.Radius;
            spell.Length = data.Indicator.Length;

            return spell;
        }
    }
}
