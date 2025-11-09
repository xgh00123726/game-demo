using GameBase.Tools;

namespace GameBase.EntitySystem
{
    public abstract class ConstructorFactory<T_DataType, T_Entity, T_Factory> : Singleton<T_Factory>
        where T_Factory : ConstructorFactory<T_DataType, T_Entity, T_Factory>, new()
    {
        public virtual T_Entity Get(T_DataType data)
        {
            return default;
        }
    }
}
