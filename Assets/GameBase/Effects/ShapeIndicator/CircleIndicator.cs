using GameBase.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.Effects
{
    public class CircleIndicator : PoolablePrefab
    {
        protected GameObject _side;

        public float Radius
        {
            get => _side.transform.localScale.x / 2;
            set => _side.transform.localScale = new Vector3(value * 2, value * 2, value * 2);
        }

        public void PlayAt(Vector3 position)
        {
            transform.position = position;
            _side.SetActive(true);
        }

        public void Hide()
        {
            _side.SetActive(false);
        }

        protected virtual void Awake()
        {
            _side = transform.Find("Side").gameObject;
            Assert.IsNotNull(_side);
        }
    }
}
