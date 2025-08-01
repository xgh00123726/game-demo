using UnityEngine;

namespace GameBase.Spell
{
    public interface ISpeller
    {
        // 需具备技能急速属性，单位1%
        float CoolingAccelerate { get; }
        GameObject GameObject { get; }
    }
}
