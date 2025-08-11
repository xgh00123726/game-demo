using UnityEngine;

namespace GameBase.UI
{
    public interface IHealthBarOwner
    {
        bool HPChange { get; set; }
        float CurrHP { get; }
        float MaxHP {  get; }
        Vector3 HealthBarPosition { get; }
    }
}
