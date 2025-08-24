using GameBase.Spells;
using UnityEngine;

namespace Constructor.Spells.Interactive
{
    public interface ISpellIndicator
    {
        public enum Type
        {
            Circle,
            Linear,
            Rect,
            FixedLinear,
        }
        void Show();
        void Hide();
        void Update(ISpeller speller, Vector3 position);
    }
}
