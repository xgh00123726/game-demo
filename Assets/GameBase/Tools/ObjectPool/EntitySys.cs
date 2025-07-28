using Unity.VisualScripting.TextureAssets;
using UnityEngine;

namespace GameBase.Tools
{
    public abstract class EntitySys<T_entity, T_obj> : UnPoolableObjectEntitySys<T_entity, T_obj>
        where T_entity : IPoolableObject, IEntity<T_obj>, new()
    {
        private ObjectPool<T_obj>[] _objPools;
        protected abstract int IDCount { get; }
        protected abstract T_obj InstantiateObject(int id);

        protected override T_obj GetObj(int id)
        {
            if (_objPools[id] == null)
            {
                _objPools[id] = new ObjectPool<T_obj>()
                {
                    InstantiateObject = () => InstantiateObject(id),
                };
            }

            return _objPools[id].Get();
        }

        protected override void Awake()
        {
            base.Awake();

            _objPools = new ObjectPool<T_obj>[IDCount];
        }
    }
}
