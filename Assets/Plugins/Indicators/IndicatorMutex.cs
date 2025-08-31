using GameBase.Tools;
using System.Collections.Generic;

namespace GameBase.Indicators
{
    public class IndicatorMutex
    {
        private List<IMutexIndicator> _indicators = new();

        public void HideOthers(IMutexIndicator indicator)
        {
            foreach (var e in _indicators)
            {
                if (e == indicator)
                {
                    continue;
                }
                e.Hide();
            }
        }

        public void HideAll()
        {
            foreach (var e in _indicators)
            {
                e.Hide();
            }
        }

        public void Add(IMutexIndicator indicator)
        {
            _indicators.Add(indicator);
            indicator.OnShow += () => HideOthers(indicator);
        }
    }
}
