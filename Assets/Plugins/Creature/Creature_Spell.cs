using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creatures
{
    public partial class Creature :
        ISpeller
    {
        public DynInventory<Spell> Spells { get; set; } = new();

        float ISpeller.CoolingAccelerate => _modifyables["coolingAccelerate"].Value;

        ISpellCamp ISpeller.Camp => Camp;

        public void AddSpell(Spell spell)
        {
            if (spell == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("try to add null spell to creature");
                return;
            }
            var i = Spells.Add(spell);
            spell.Speller = this;
        }

        public Spell GetSpell(int index)
        {
            if (Spells.HasItem(index))
            {
                return Spells[index];
            }

            return null;
        }

        void ISpeller.LookAt(Vector3 pos)
        {
            Interrupt();
            Mover.LookAt(pos);
        }
    }
}
