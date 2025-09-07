using System.Collections.Generic;

namespace GameBase.Inventorys
{
    public interface IDataBase<T_Data>
    {
        int Count { get; }
        T_Data Read(int index);
        void Write(T_Data data, int index);
    }
}
