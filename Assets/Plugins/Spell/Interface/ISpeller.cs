using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Spells
{
    public interface ISpeller
    {
        // 需具备技能急速属性，单位1%
        float CoolingAccelerate { get; }
        Vector3 Position { get; }
        Vector3 Dir { get; }
        void LookAt(Vector3 pos);
        ISpellCamp Camp { get; }
    }
}
