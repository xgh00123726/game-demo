using GameBase.Indicators;
using GameBase.Spells;
using UnityEngine;

public interface IInteractiveIndicator : IMutexIndicator
{
    void Show();
    void Update(ISpeller speller, Vector3 position);
}
