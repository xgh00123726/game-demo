using UnityEngine;

namespace GameBase.Inventorys
{
    public class WeightInventory<T>
    {
        public struct WeightedType
        {
            public T value;
            public int weight;
        }

        private WeightedType[] _datas;
        private int _weightSum;

        public WeightInventory(WeightedType[] datas)
        {
            ReInit(datas);
        }

        public void ReInit(WeightedType[] datas)
        {
            _datas = datas;
            _weightSum = 0;
            for (int i = 0; i < _datas.Length; i++)
            {
                _weightSum += _datas[i].weight;
            }
        }

        public int GetRandom(out T value)
        {
            int randomInt = Random.Range(0, _weightSum - 1);
            if (randomInt < 0 || _datas.Length == 0)
            {
                value = default;
                return -1;
            }

            for (int i = 0; i < _datas.Length; i++)
            {
                randomInt -= _datas[i].weight;
                if (randomInt < 0)
                {
                    value = _datas[i].value;
                    return i;
                }
            }

            value = default;
            return -1;
        }
    }
}
