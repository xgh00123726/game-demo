using GameBase.Spells;
using UnityEngine;

public interface ISpellIndicator
{
    void Show();
    void Hide();
    void Update(ISpeller speller, Vector3 position);
}
