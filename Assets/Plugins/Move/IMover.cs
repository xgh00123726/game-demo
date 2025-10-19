using UnityEngine;

namespace GameBase.Move
{
    public interface IMover
    {
        float MoveSpeed { get; }
        float RotateSpeed { get; }
        Vector3 Position { get; set; }
        float Radius { get; }
        GameObject Obj { get; }
        Vector3 Dir { get; set; }
        CircleCollider Collider { get; }
    }
}
