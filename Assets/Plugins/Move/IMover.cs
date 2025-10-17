using UnityEngine;

namespace GameBase.Move
{
    public interface IMover
    {
        float Speed { get; }
        Vector3 Position { get; set; }
        float Radius { get; }
        CircleCollider Collider { get; }
    }
}
