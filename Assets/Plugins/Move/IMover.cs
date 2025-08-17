using UnityEngine;

namespace GameBase.Move
{
    public interface IMover
    {
        float Speed { get; }
        GameObject GO { get; }
        Vector3 Dest { get; set; }
        bool IsMoving { get; set; }
    }
}
