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
        public Vector3 BuffPositionDelta(int index)
        {
            return new Vector3(32 * index, 0, 0);
        }
        public void ShowBuff(IViewableBuff buff)
        {
            var buffItem = PoolableMonoMgr<BuffItem>.Instance.Get();
            buffItem.Bind(buff);
            buffItem.transform.SetParent(_UIComponents.transform, false);
            buffItem.transform.localPosition = BuffPositionDelta(PoolableMonoMgr<BuffItem>.Instance.Pool.ActiveList.Count);
        }

        private void Update()
        {
            for (int i = 0; i < PoolableMonoMgr<BuffItem>.Instance.Pool.ActiveList.Count; ++i)
            {
                BuffItem item = PoolableMonoMgr<BuffItem>.Instance.Pool.ActiveList[i];
                item.transform.localPosition = BuffPositionDelta(i);
            }
        }
    }
}
