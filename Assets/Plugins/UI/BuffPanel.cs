using GameBase.Buff;
using GameBase.Resources;
using GameBase.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class BuffPanel : BaseUI
    {
        private static GameObject _UIComponents;
        private static UObjectPool<BuffItem> _buffItems = new UObjectPool<BuffItem>();
        public static Vector3 BuffPositionDelta(int index)
        {
            return new Vector3(32 * index, 0, 0);
        }


        private static BuffItem GetBuffItem(UObjectPool<BuffItem> pool)
        {
            if (pool.Empty)
            {
                // 9,Prefabs/UI/BuffItem
                var item = GameObject.Instantiate(ResourcesLoader.GetPrefab(9)).AddComponent<BuffItem>();
                pool.Add(item);
                return item;
            }
            else
            {
                return pool.Get();
            }
        }

        public static void ShowBuff(IViewableBuff buff)
        {
            var buffItem = GetBuffItem(_buffItems);
            buffItem.Bind(buff);
            buffItem.transform.SetParent(_UIComponents.transform, false);
            buffItem.transform.localPosition = BuffPositionDelta(_buffItems.Count);
        }

        private void Awake()
        {
            _UIComponents = transform.Find("UIComponents").gameObject;
            if (_UIComponents == null)
            {
                Debug.LogWarning("A panel must has a UIComponents");
            }
            Assert.IsNotNull(_UIComponents);

            _buffItems.InstantiateAction = BuffItem.OnInstantiate;
            _buffItems.ReleaseAction = BuffItem.OnRelease;
        }

        private void Update()
        {
            int idx = 0;
            foreach (var buff in _buffItems)
            {
                buff.transform.localPosition = BuffPositionDelta(idx++);
            }
        }
    }
}
