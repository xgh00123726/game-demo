using UnityEngine;

namespace GameBase.Triggers
{
    public interface ITriggerTarget
    {
        Vector3 Position { get; }
        float Radius { get; }
    }
}
