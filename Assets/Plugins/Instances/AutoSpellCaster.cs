using Constructor.Spells.Interactive;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Spells;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class AutoSpellCaster : SingletonInstance<AutoSpellCaster>
    {
        private static List<Spell> _spells = new();

        public static void Register(Spell spell)
        {
            _spells.Add(spell);
        }

        public static void Unregister(Spell spell)
        {
            _spells.Remove(spell);
        }

        public static void CastByStyle(Spell spell)
        {
            if (spell.interactive is DotExternalSet dot)
            {
                if (dot.data.style == DotExternalSetStyle.NearestTarget)
                {
                    var c = CreatureSys.Instance.NearestEntity(spell.speller.Position, dot.data.targetTag);
                    if (c != null)
                    {
                        dot.position = c.Position;
                    }
                }
                else if (dot.data.style == DotExternalSetStyle.Random)
                {
                    float x = Random.Range(dot.data.randomMinX, dot.data.randomMaxX);
                    float y = Random.Range(dot.data.randomMinY, dot.data.randomMaxY);
                    float z = Random.Range(dot.data.randomMinZ, dot.data.randomMaxZ);
                    dot.position = new Vector3(x, y, z);
                }
            }
            spell.TryCast();
        }

        protected override void Update()
        {
            foreach (var spell in _spells)
            {
                spell.TryCast();
            }
        }
    }
}
