using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public interface ISpeller
    {
        // 需具备技能急速属性，单位1%
        float CoolingAccelerate { get; }
        Vector3 Position { get; }
        SpellContainer SpellContainer { get; }
    }
}
