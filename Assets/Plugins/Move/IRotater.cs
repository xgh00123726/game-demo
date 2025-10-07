using UnityEngine;

namespace GameBase.Move
{
    public interface IRotater
    {
        float Speed { get; }
        GameObject Obj { get; }
        Vector3 Dir { get; set; }
        bool IsRotating { get; set; }
    }
}
