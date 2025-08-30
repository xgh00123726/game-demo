using GameBase.Spells;
using UnityEngine;

public interface IInteractiveIndicator
{
    void Show();
    void Hide();
    void Update(ISpeller speller, Vector3 position);
}
