using System.Collections;
using System.Collections.Generic;
using GameBase.Buff;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.UI
{
    public class BuffPanel : BasePanel
    {
        public static BuffPanel Instance => _instance;
        internal static BuffPanel _instance;
        PoolableMonoMgr<BuffItem> _buffItemMgr;
        public Vector3 BuffPositionDelta(int index)
        {
            return new Vector3(32 * index, 0, 0);
        }
        public void ShowBuff(IViewableBuff buff)
        {
            var buffItem = _buffItemMgr.Get();
            buffItem.Bind(buff);
            buffItem.transform.SetParent(_UIComponents.transform, false);
            buffItem.transform.localPosition = BuffPositionDelta(_buffItemMgr.Pool.ActiveList.Count);
        }

        protected override void Awake()
        {
            base.Awake();
            _buffItemMgr = PoolableMonoMgr<BuffItem>.Instance(PrefabType.UI);
        }

        private void Update()
        {
            int idx = 0;
            foreach (var buff in _buffItemMgr.Pool.ActiveList)
            {
                buff.transform.localPosition = BuffPositionDelta(idx++);
            }
        }
    }
}
