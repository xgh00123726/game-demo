using GameBase.Indicators;
using GameBase.Spells;
using UnityEngine;

namespace Constructor.Spells.Indicators
{
    public class CircleIndicator : IInteractiveIndicator
    {
        public Indicator indicator;

        void IInteractiveIndicator.Hide()
        {
            indicator.Obj.SetActive(false);
        }
        void IInteractiveIndicator.Update(ISpeller speller, Vector3 position)
        {
            indicator.Obj.transform.position = position;
        }
        void IInteractiveIndicator.Show()
        {
            indicator.Obj.SetActive(true);
        }
    }
}
