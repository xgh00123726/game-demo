using GameBase.Inventorys;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creatures
{
    public partial class Creature :
        ISpeller
    {
        public DynInventory<Spell> spells = new();

        float ISpeller.CoolingAccelerate => modifyables["coolingAccelerate"].Value;

        ISpellCamp ISpeller.Camp => camp;

        public void AddSpell(Spell spell)
        {
            if (spell == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("try to add null spell to creature");
                return;
            }
            var i = spells.Add(spell);
            spell.speller = this;
        }

        public Spell GetSpell(int index)
        {
            if (spells.HasItem(index))
            {
                return spells[index];
            }

            return null;
        }

        void ISpeller.LookAt(Vector3 pos)
        {
            Interrupt();
            mover.LookAt(pos);
        }
    }
}
