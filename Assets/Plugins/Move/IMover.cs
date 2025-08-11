using UnityEngine;

namespace GameBase.Move
{
    public interface IMover
    {
        float moveSpeed { get; }
        GameObject GO { get; }
    }
}
