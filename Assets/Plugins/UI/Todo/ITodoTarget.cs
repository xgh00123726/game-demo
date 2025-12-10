using UnityEngine;

namespace GameBase.UI
{
    public interface ITodoTarget
    {
        Vector3 Position { get; set; }
        bool IsShow { get; }
    }
}
