using GameBase.Creatures;
using GameBase.Tools;
using GameBase.Indicators;
using GameBase.Inventorys;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class DrawCreatureRadius : SingletonInstance<DrawCreatureRadius>
    {
        protected static List<Indicator> _indicators = new();

        public static Color DefaultColor { get; set; } = Color.white;
        public static Dictionary<Creature, Color> ColorSet { get; set; } = new();

        private static void FillIndicatorSize(int size)
        {
            for (int i = _indicators.Count; i < size; i++)
            {
                _indicators.Add(IndicatorFactory.Get(IndicatorType.Circle));
            }
        }

        protected override void Update()
        {
            var sys = CreatureSys.Instance;
            
            if (sys == null)
            {
                return;
            }

            FillIndicatorSize(sys.Entities.Count);

            int i = 0;
            foreach (var c in sys.Entities)
            {
                var indicator = _indicators[i++];
                indicator.Size = new UnityEngine.Vector3(c.Radius, c.Radius, c.Radius);
                indicator.Obj.transform.position = c.Position;

                if (ColorSet.ContainsKey(c))
                {
                    indicator.Color = ColorSet[c];
                }
                else
                {
                    indicator.Color = DefaultColor;
                }

                indicator.Obj.SetActive(true);
            }
            for (; i < _indicators.Count; i++)
            {
                _indicators[i].Obj.SetActive(false);
            }
        }
    }
}
