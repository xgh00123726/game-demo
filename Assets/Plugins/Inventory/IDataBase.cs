using System.Collections.Generic;

namespace GameBase.Inventorys
{
    public interface IDataBase<T_Data>
    {
        void Read(out T_Data data);
        void Read(out IEnumerator<T_Data> datas);
        void Write(T_Data data);
        void Write(IEnumerator<T_Data> datas);
    }
}
