using UnityEngine;
namespace GameBase.Animations
{
    public interface IAnimatable
    {
        Animator Animator { get; }
        bool IsMoving();
        bool IsIdle();
    }
}
