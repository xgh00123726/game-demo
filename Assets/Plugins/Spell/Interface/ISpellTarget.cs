using UnityEngine;

namespace GameBase.Spells
{
    public interface ISpellTarget
    {
        Vector3 Center { get; }
        float Radius { get; }
    }
}
