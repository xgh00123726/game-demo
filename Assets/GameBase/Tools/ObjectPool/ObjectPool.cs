using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    // 管理类型T的对象，T必须是可对象池化的
    public class ObjectPool<T> where T : IPoolableObject
    {
        private List<T> _activeList = new List<T>();
        private List<T> _activeBuffer = new List<T>();
        private List<T> _releasedList = new List<T>();
        private List<T> _releasedBuffer = new List<T>();

        public List<T> ActiveList => _activeList;

        public delegate T InstantiateObjectAction();
        private InstantiateObjectAction _InstantiateObject;
        public InstantiateObjectAction InstantiateObject { set =>  _InstantiateObject = value; }

        // 对象池是否为空
        public bool Empty { get => _releasedList.Count == 0; }
        // 向对象池中获取一个对象
        public T Get()
        {
            if (_InstantiateObject == null)
            {
                Logger.Level(Logger.LogLevel.Warning)
                    .Log("before get a object from object pool, you shold set the instantiate method by setter 'InstantiateObject'");
                return default;
            }
            if(_releasedList.Count > 0)
            {
                var obj = _releasedList[0];
                obj.OnInstantiate();

                _activeList.Add(obj);
                _releasedList.RemoveAt(0);
                return obj;
            }
            var objInstantiate = _InstantiateObject();
            objInstantiate.OnInstantiate();

            _activeList.Add(objInstantiate);

            return objInstantiate;
        }

        // 将对象释放回对象池，会触发告警
        public void Release(T obj)
        {
            int objIndex = _activeList.IndexOf(obj);
            if (objIndex == -1)
            {
                Debug.LogWarning("object pool release unknown object");
                return;
            }
            obj.OnRelease();
            _activeList.RemoveAt(objIndex);
            _releasedList.Add(obj);
        }

        // 尝试将对象放回对象池，不会触发告警
        public void TryRelease(T obj)
        {
            int objIndex = _activeList.IndexOf(obj);
            if (objIndex == -1)
            {
                return;
            }
            obj.OnRelease();
            _activeList.RemoveAt(objIndex);
            _releasedList.Add(obj);
        }

        public void ReleaseToBuffer(T obj)
        {
            _releasedBuffer.Add(obj);
        }

        public void FlushReleaseBuffer()
        {
            foreach(var item in _releasedBuffer)
            {
                int itemIndex = _activeList.IndexOf(item);
                if (itemIndex == -1)
                {
                    Debug.LogWarning("object pool release unknown object");
                    continue;
                }
                item.OnRelease();
                _activeList.RemoveAt(itemIndex);
                _releasedList.Add(item);
            }
            _releasedBuffer.Clear();
        }
    }

}