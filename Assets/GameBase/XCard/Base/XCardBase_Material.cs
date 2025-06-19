using GameBase.GCamera;
using GameBase.Resources;
using UnityEngine;


namespace GameBase.XCard
{
    public partial class XCardBase : PoolablePrefab
    {
        public Vector2 tillingSet = new Vector2(0.22f, 0.55f);
        public Vector2 offsetSet = new Vector2(0f, 0.22f);
        public float spadeOffset = 0f;
        public float diamondOffset = 0.26f;
        public float clubOffset = 0.525f;
        public float heartOffset = 0.78f;
        public enum SuitType
        {
            Spade = 0,
            Heart = 1,
            Diamond = 2,
            Club = 3,
        }

        private void SuitSetScale(Vector2 scale)
        {
            suitMaterialTop.SetTextureScale("_MainTex", scale);
            suitMaterialBottom.SetTextureScale("_MainTex", scale);
        }

        private void SuitSetOffset(Vector2 offset)
        {
            suitMaterialTop.SetTextureOffset("_MainTex", offset);
            suitMaterialBottom.SetTextureOffset("_MainTex", offset);
        }

        private void MaterialInit()
        {
            SuitSetScale(tillingSet);
            SuitSetOffset(offsetSet);
        }

        // ºÚÌÒ(Spade)¡¢ºìÌÒ(Heart)¡¢·½¿é(Diamond)¡¢²Ý»¨(Club)
        private void SetSuit(SuitType suit)
        {
            if (suit == SuitType.Spade)
            {
                SuitSetOffset(new Vector2(spadeOffset, offsetSet.y));
            }
            else if (suit == SuitType.Diamond)
            {
                SuitSetOffset(new Vector2(diamondOffset, offsetSet.y));
            }
            else if (suit == SuitType.Club)
            {
                SuitSetOffset(new Vector2(clubOffset, offsetSet.y));
            }
            else if (suit == SuitType.Heart)
            {
                SuitSetOffset(new Vector2(heartOffset, offsetSet.y));
            }
            else
            {

            }
        }
    }
}