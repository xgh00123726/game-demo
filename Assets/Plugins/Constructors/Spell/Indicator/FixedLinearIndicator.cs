using GameBase.Indicators;
using GameBase.Spells;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Indicators
{
    public class FixedLinearIndicator : IInteractiveIndicator
    {
        public Indicator indicator;

        protected bool _fixedLength = true;
        public float Length
        {
            set
            {
                _fixedLength = true;
                SetLength(value);
            }
        }

        private void SetLength(float value)
        {
            indicator.Size = new Vector3(0.2f, value);
            indicator.Pivot = new Vector3(0f, value / 2, 0f);
        }

        void IInteractiveIndicator.Hide()
        {
            indicator.Obj.SetActive(false);
        }

        void IInteractiveIndicator.Update(ISpeller speller, Vector3 position)
        {
            indicator.Obj.transform.position = speller.Position;
            Vector3 dir = position - speller.Position;
            float x = dir.x;
            float z = dir.z;
            float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
            indicator.Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            if (!_fixedLength)
            {
                SetLength(dir.magnitude);
            }
        }

        void IInteractiveIndicator.Show()
        {
            indicator.Obj.SetActive(true);
        }
    }
}
