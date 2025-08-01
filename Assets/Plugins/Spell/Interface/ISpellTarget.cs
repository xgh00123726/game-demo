using UnityEngine;

namespace GameBase.Spell
{
    public interface ISpellTarget
    {
        Vector3 Center { get; }
        float Radius { get; }
    }
}
