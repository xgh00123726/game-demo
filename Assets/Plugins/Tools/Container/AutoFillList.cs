using System.Collections.Generic;

namespace GameBase.Tools
{
    public class AutoFillList<T> : List<T>
    {
        public void Add(T e, int index)
        {
            if (index < Count)
            {
                this[index] = e;
            }
            else
            {
                Capacity = index + 1;
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
