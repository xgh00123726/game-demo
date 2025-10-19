using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    public class PriorityQueueLogger
    {
        public static int Pow2(int x)
        {
            int ret = 1;
            for (int i = 0; i < x; i++) 
            {
                ret *= 2;
            }
            return ret;
        }
        public static void Log<T>(PriorityQueue<T> q) where T : IComparable<T>
        {
            var size = q._elems.Count;
            int tier = (int)System.MathF.Log(size, 2) + 1;
            int i = 0;
            for (int t = 0; t < tier; t++)
            {
                string o = $"tier{t}: ";
                for (; i < Mathf.Min(size + 1, Pow2(t + 1)) - 1; i++)
                {
                    o += $"  -{q._elems[i]}-  ";
                }
                XLogger.Instance.ToFile("queueLog.html").Log(o);
            }
        }
    }
    public class PriorityQueue<T> where T : IComparable<T>
    {
        protected internal List<T> _elems = new();

        private void Swap(int p1, int p2)
        {
            (_elems[p1], _elems[p2]) = (_elems[p2], _elems[p1]);
        }

        private void UpHeap(int index)
        {
            var pIndex = (index - 1) / 2;
            if (_elems[pIndex].CompareTo(_elems[index]) < 0)
            {
                Swap(index, pIndex);
                UpHeap(pIndex);
            }
        }

        private void DownHeap(int index)
        {
            int maxIndex = index;
            int lIndex = index * 2 + 1;
            int rIndex = index * 2 + 2;
            if (lIndex < _elems.Count && _elems[maxIndex].CompareTo(_elems[lIndex]) < 0)
            {
                maxIndex = lIndex;
            }
            if (rIndex < _elems.Count && _elems[maxIndex].CompareTo(_elems[rIndex]) < 0)
            { 
                maxIndex = rIndex;
            }
            if (maxIndex != index)
            {
                Swap(index, maxIndex);
                DownHeap(maxIndex);
            }
        }

        public void Enqueue(T item)
        {
            if (_elems.Count == 0)
            {
                _elems.Add(item);
                return;
            }
            else
            {
                _elems.Add(item);
                UpHeap(_elems.Count - 1);
            }
        }

        public T Dequeue()
        {
            if ( _elems.Count == 0)
            {
                return default;
            }

            var ret = _elems[0];
            _elems[0] = _elems[_elems.Count - 1];
            _elems.RemoveAt(_elems.Count - 1);
            DownHeap(0);
            return ret;
        }

        public T Peek()
        {
            if (_elems.Count == 0)
            {
                return default;
            }
            return _elems[0];
        }

        public void Clear()
        {
            _elems.Clear();
        }
    }
}
