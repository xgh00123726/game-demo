using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    // 管理类型T的对象，T必须是可对象池化的
    public class PoolableObjectPool<T> where T : IPoolableObject
    {
        private LinkedList<T> _activeList = new LinkedList<T>();
        private LinkedList<T> _activeBuffer = new LinkedList<T>();
        private LinkedList<T> _releasedList = new LinkedList<T>();
        private LinkedList<T> _releasedBuffer = new LinkedList<T>();

        public LinkedList<T> ActiveList => _activeList;
        public LinkedList<T> ReleasedList => _releasedList;

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
                Logger.Instance.Level(Logger.LogLevel.Warning)
                    .Log("before get a object from object pool, you shold set the instantiate method by setter 'InstantiateObject'");
                return default;
            }
            if(_releasedList.Count > 0)
            {
                var obj = _releasedList.First.Value;
                obj.OnInstantiate();

                _activeList.AddLast(obj);
                _releasedList.Remove(obj);
                return obj;
            }
            var objInstantiate = _InstantiateObject();
            objInstantiate.OnInstantiate();

            _activeList.AddLast(objInstantiate);

            return objInstantiate;
        }

        /// <summary>
        /// 将一个对象强制放回对象池
        /// <list type="bullet">
        /// <item><param name="obj">需要被放回的对象</param></item>
        /// </list></summary>
        public void Release(T obj)
        {
            var node = _activeList.Find(obj);
            obj.OnRelease();
            if (node == null)
            {
                _releasedList.AddLast(obj);
            }
            else
            {
                _activeList.Remove(obj);
                _releasedList.AddLast(obj);
            }
        }

        /// <summary>
        /// 尝试讲一个对象放回对象池
        /// <list type="bullet">
        /// <item><param name="obj">需要被放回的对象</param></item>
        /// </list></summary>
        /// <returns>是否销毁成功</returns>
        public bool TryRelease(T obj)
        {
            var node = _activeList.Find(obj);
            
            if (node == null)
            {
                return false;
            }
            else
            {
                obj.OnRelease();
                _activeList.Remove(obj);
                _releasedList.AddLast(obj);
            }

            return true;
        }

        public void ReleaseToBuffer(T obj)
        {
            _releasedBuffer.AddLast(obj);
        }

        public void FlushReleaseBuffer()
        {
            foreach(var item in _releasedBuffer)
            {
                Release(item);
            }
            _releasedBuffer.Clear();
        }
    }

}