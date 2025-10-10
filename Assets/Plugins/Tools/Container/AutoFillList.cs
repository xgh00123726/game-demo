using System.Collections.Generic;

namespace GameBase.Tools
{
    public class AutoFillList<T> : List<T>
    {
        public void CopyFrom(List<T> list)
        {
            int size = list.Count;
            for (int i = 0; i < size; i++)
            {
                this[i] = list[i];
            }
        }

        public void Resize(int size, T value = default)
        {
            int count = Count;
            if (size > count)
            {
                for (int i = 0; i < size - count; i++)
                {
                    Add(value);
                }
            }
        }

        public void Add(T e, int index)
        {
            if (index < Count)
            {
                this[index] = e;
            }
            else
            {
                int count = Count;
                for (int i = 0; i < index - count + 1; i++)
                {
                    Add(default);
                }
                this[index] = e;
            }
        }
    }
}
