using System;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class ObjectPool<T>
    {
        private List<T> _activeList = new List<T>();
        private List<T> _releasedList = new List<T>();

        public List<T> ActiveList => _activeList;
        public List<T> ReleasedList => _releasedList;

        public delegate T InstantiateObjectAction();
        private InstantiateObjectAction _InstantiateObject;
        public InstantiateObjectAction InstantiateObject { set => _InstantiateObject = value; }

        // 对象池是否为空
        public bool Empty { get => _releasedList.Count == 0; }
        // 向对象池中获取一个对象
        public T Get()
        {
            if (_InstantiateObject == null)
            {
                Logger.Instance.Level(Logger.LogLevel.Warning)
                    .Log("before get a object from object pool, you shold set the instantiate method by setter 'InstantiateObject'");
                return default;
            }
            if (_releasedList.Count > 0)
            {
                var obj = _releasedList[0];

                _activeList.Add(obj);
                _releasedList.RemoveAt(0);
                return obj;
            }
            var objInstantiate = _InstantiateObject();

            _activeList.Add(objInstantiate);

            return objInstantiate;
        }

        /// <summary>
        /// 将一个对象强制放回对象池
        /// <list type="bullet">
        /// <item><param name="obj">需要被放回的对象</param></item>
        /// </list></summary>
        public void Release(T obj)
        {
            int objIndex = _activeList.IndexOf(obj);
            if (objIndex == -1)
            {
                _releasedList.Add(obj);
                return;
            }
            _activeList.RemoveAt(objIndex);
            _releasedList.Add(obj);
        }
    }
}
