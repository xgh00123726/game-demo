using GameBase.Indicators;
using GameBase.Spells;
using System;
using UnityEngine;

namespace Constructor.Spells.Indicators
{
    public class CircleIndicator : IInteractiveIndicator
    {
        public Indicator indicator;

        public System.Action OnShow { get; set; }

        void IInteractiveIndicator.Update(ISpeller speller, Vector3 position)
        {
            indicator.Obj.transform.position = position;
        }
        void IInteractiveIndicator.Show()
        {
            indicator.Obj.SetActive(true);
            OnShow?.Invoke();
        }

        void IMutexIndicator.Hide()
        {
            indicator.Obj.SetActive(false);
        }
    }
}
