using GameBase.Indicators;
using GameBase.Spells;
using System;
using UnityEngine;

namespace GameBase.Indicators
{
    public class CircleIndicator : CastIndicator
    {
        public Indicator indicator;

        public override void Show()
        {
            indicator.Obj.SetActive(true);
        }

        public override void Hide()
        {
            indicator.Obj.SetActive(false);
        }

        public override void Set(IndicatorConfig config)
        {
            indicator.Obj.transform.position = config.position;
        }
    }
}
