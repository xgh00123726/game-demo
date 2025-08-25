using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.Modify
{
    public class ConvientModifyerFunc
    {
        private static Dictionary<int, Func<float, float, float>> _floatCurrPercentDict = new();
        private static Dictionary<int, Func<float, float, float>> _floatSetPercentDict = new();
        private static Dictionary<int, Func<float, float, float>> _floatFixedValueDict = new();

        public static Func<float, float, float> FloatCurrPercent(float percent)
        {
            return (float curr, float vset) =>
            {
                return curr + curr * percent / 100;
            };
        }

        public static Func<float, float, float> FloatSetPercent(float percent)
        {
            return (float curr, float vset) =>
            {
                return curr + vset * percent / 100;
            };
        }

        public static Func<float, float, float> FloatFixedValue(float value)
        {
            return (float curr, float vset) =>
            {
                return curr + value;
            };
        }

        public static Func<float, float, float> FloatCurrPercent(int percent)
        {
            if (!_floatCurrPercentDict.ContainsKey(percent))
            {
                _floatCurrPercentDict.Add(percent, (float curr, float vset) =>
                {
                    return curr + curr * percent / 100f;
                });
            }

            return _floatCurrPercentDict[percent];
        }

        public static Func<float, float, float> FloatSetPercent(int percent)
        {
            if (!_floatSetPercentDict.ContainsKey(percent))
            {
                _floatSetPercentDict.Add(percent, (float curr, float vset) =>
                {
                    return curr + vset * percent / 100f;
                });
            }

            return _floatSetPercentDict[percent];
        }

        public static Func<float, float, float> FloatFixedValue(int value)
        {
            if (!_floatFixedValueDict.ContainsKey(value))
            {
                _floatFixedValueDict.Add(value, (float curr, float vset) =>
                {
                    return curr + value;
                });
            }

            return _floatFixedValueDict[value];
        }
    }
}
