using GameBase.Indicators;
using GameBase.Spells;
using NReco.Csv;
using System;
using UnityEngine;

namespace Constructor.Spells.Indicators
{

    public class RectIndicator : IInteractiveIndicator
    {
        public Indicator indicator;

        public Vector3 Dir
        {
            set
            {
                float x = value.x;
                float z = value.z;
                float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
                indicator.Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            }
            get
            {
                return indicator.Obj.transform.right;
            }
        }

        void IInteractiveIndicator.Hide()
        {
            indicator.Obj.SetActive(false);
        }

        void IInteractiveIndicator.Show()
        {
            indicator.Obj.SetActive(true);
        }

        void IInteractiveIndicator.Update(ISpeller speller, Vector3 position)
        {
            indicator.Obj.transform.position = position;
        }
    }
}
