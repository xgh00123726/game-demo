using UnityEngine;

namespace GameBase.Spells
{
    public interface ISpellInteractive
    {
        void OnTrig();
        Vector3 TrigPosition {  get; }
    }
}
