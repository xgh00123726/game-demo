using GameBase.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameBase.Shops
{
    public class ShopModel
    {
        internal int goodNums = 5;
        internal List<int> currGoodIDs = new();
        internal List<int> goodIDs = new();
        internal List<int> weights = new();
        internal List<int> prices = new();
        internal int weightSum = 0;

        public void SetGoods(List<int> goodIDs, List<int> weights, List<int> prices)
        {
            this.goodIDs = goodIDs;
            this.weights = weights;
            this.prices = prices;
            weightSum = this.weights.Sum();
        }

        public int GetGoodID(int index)
        {
            return goodIDs[index];
        }

        internal int GoodNums
        {
            get => goodNums;
            set
            {
                if (value >= goodNums)
                {
                    for(int i = 0; i < value - goodNums; i++)
                    {
                        currGoodIDs.Add(-1);
                    }
                }
                else if (value < goodNums)
                {
                    currGoodIDs.RemoveRange(value, goodNums - value);
                }
                goodNums = value;
            }
        }

        /// <summary>
        /// 根据随机数和权重获取一个随机的商品ID下标，随机数范围[0,weightSum)
        /// </summary>
        /// <param name="randomInt"></param>
        /// <param name="weightSum"></param>
        /// <param name="goodIDs"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        private int RandomToGoodIndex(int randomInt, List<int> weights)
        {
            if (randomInt <= 0 || weights.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < weights.Count; i++)
            {
                randomInt -= weights[i];
                if (randomInt < 0)
                {
                    return i;
                }
            }

            return -1;
        }

        protected internal virtual bool Purchase(int index, IShoper shoper)
        {
            if (index < 0 || index >= goodNums)
            {
                return false;
            }
            if (shoper.Gold < prices[index])
            {
                return false;
            }

            currGoodIDs[index] = -1;
            return true;
        }

        internal void Refresh()
        {
            for (int i = 0; i < goodNums; ++i)
            {
                int randInt = Random.Range(0, weightSum - 1);
                int index = RandomToGoodIndex(randInt, weights);
                currGoodIDs[i] = goodIDs[index];
            }
        }
    }
}
