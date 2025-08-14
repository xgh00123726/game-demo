using UnityEngine;

namespace GameBase.UI
{
    public interface IHealthBarOwner
    {
        float CurrHP { get; }
        float MaxHP {  get; }
        Vector3 HealthBarPosition { get; }
        bool ALive { get; }
    }
}
