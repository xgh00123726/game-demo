using UnityEngine;

namespace GameBase.Triggers
{
    public interface ITriggerTarget
    {
        Vector3 Center { get; }
        float Radius { get; }
    }
}
