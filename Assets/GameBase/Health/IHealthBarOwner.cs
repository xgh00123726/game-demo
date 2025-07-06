using UnityEngine;

namespace GameBase.Health
{
    public interface IHealthBarOwner
    {
        Vector3 HealthBarPosition { get; }
    }
}
