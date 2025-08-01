using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyMgr : MonoBehaviour
    {
        static ModifyContainer _container = new ModifyContainer();
        static List<Modify> _removeList = new List<Modify>();
        #region debug
        static ModifyMgr _instance;
        public int modifyCount = 0;
        public int addedCount = 0;
        public int removedCount = 0;
        #endregion
        
        internal static void Add(Modify modify)
        {
            _container.Add(modify);
            _instance.addedCount++;
            _instance.modifyCount = _container._modifies.Count;
        }
        internal static void Remove(Modify modify)
        {
            _removeList.Add(modify);
            _instance.removedCount++;
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
        void Update()
        {
            // 每个Modify帧进行前把所有Modify消除
            foreach (Modify modify in _container._modifies)
            {
                modify.Reset();
            }

            // 移除需要被移除的Modify
            // 移除需要发生在reset后，update前，否则会导致Modify永久存在
            foreach (Modify modify in _removeList)
            {
                _container.Remove(modify);
            }
            _instance.modifyCount -= _removeList.Count;
            _removeList.Clear();

            // 重新计算Modify
            foreach (Modify modify in _container._modifies)
            {
                modify.Update();
            }
        }
    }
}
