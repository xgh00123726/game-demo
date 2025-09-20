using UnityEngine;

namespace GameBase.Spells
{
    public interface IKeySpeller
    {
        Spell GetSpell(int index);
        Vector3 Position { get; }
    }
}
