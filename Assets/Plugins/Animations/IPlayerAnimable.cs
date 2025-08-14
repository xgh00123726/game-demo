using UnityEngine;
namespace GameBase.Animations
{
    public interface IPlayerAnimable
    {
        Animator Animator { get; }
        bool IsMoving();
        bool IsIdle();
    }
}
