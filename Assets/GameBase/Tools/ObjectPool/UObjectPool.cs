using System;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class UObjectPool<T> : IEnumerable<T>,
        IEntityContainer<T>
    {
        private LinkedList<T> _activeList = new LinkedList<T>();
        private LinkedList<T> _releasedList = new LinkedList<T>();

        public Action<T> InstantiateAction;
        public Action<T> ReleaseAction;

        public LinkedList<T> ActiveList => _activeList;
        public LinkedList<T> ReleasedList => _releasedList;

        // 对象池是否为空
        public bool Empty { get => _releasedList.Count == 0; }

        public int Count => ActiveList.Count;

        protected virtual void OnInstantiate(T obj)
        {
            InstantiateAction?.Invoke(obj);
        }

        protected virtual void OnRelease(T obj)
        {
            ReleaseAction?.Invoke(obj);
        }

        // 向对象池中获取一个对象
        public T Get()
        {
            if (Empty)
            {
                Tools.Logger.Instance.Level(Logger.LogLevel.Error)
                    .Log("trying get object from a empty pool");

                return default;
            }

            var obj = _releasedList.First.Value;

            OnInstantiate(obj);
            _activeList.AddLast(obj);
            _releasedList.Remove(obj);
            return obj;
        }

        /// <summary>
        /// 将一个对象强制放回对象池
        /// <list type="bullet">
        /// <item><param name="obj">需要被放回的对象</param></item>
        /// </list></summary>
        public void Release(T obj)
        {
            if (!_activeList.Contains(obj))
            {
                Tools.Logger.Instance.Level(Logger.LogLevel.Error)
                    .Log("trying release object to a dismatch pool");
                return;
            }

            OnRelease(obj);
            _activeList.Remove(obj);
            _releasedList.AddLast(obj);
        }

        public void Add(T obj)
        {
            OnInstantiate(obj);
            _activeList.AddLast(obj);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)ActiveList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)ActiveList).GetEnumerator();
        }
    }
}
