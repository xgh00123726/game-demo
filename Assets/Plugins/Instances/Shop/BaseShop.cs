using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace Instance.Shops
{
    public class BaseShop
    {
        private int _goodNum;
        private int _prefabID;
        private Vector3 _position;
        private GameObject _obj;


        public IShoper shoper;
        public KeyFunction openKey = KeyFunction.OpenShop;
        public KeyFunction closeKey = KeyFunction.CloseShop;

        public BaseShop(int prefabID, Vector3 position, int goodNum)
        {
            PrefabID = prefabID;
            Position = position;
            GoodNum = goodNum;
        }

        public int GoodNum
        {
            get => _goodNum;
            set
            {

            }
        }
        public Vector3 Position
        {
            get => _position;
            set
            {
                _obj.transform.position = value;
            }
        }
        public int PrefabID
        {
            get => _prefabID;
            set
            {
                _prefabID = value;
                _obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(value));
            }
        }
    }
}
