using GameBase.Tools;
using UnityEngine;

namespace GameBase.EntitySystem
{
    public abstract class BaseConstructor<T_Data, T_Entity, T_Constructor> : Singleton<T_Constructor>
        where T_Data : struct
        where T_Entity : class
        where T_Constructor : BaseConstructor<T_Data, T_Entity, T_Constructor>, new()
    {
        private T_Data[] _datas;

        protected BaseConstructor()
        {
            Init();
            Command.Register($"{typeof(T_Constructor).FullName}-init", Init);
        }
        protected abstract string RelativePath { get; }
        protected abstract void Set(T_Entity e, in T_Data data);

        protected abstract T_Entity Get();

        public T_Entity Get(int index)
        {
            if (index >= _datas.Length || index < 0)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"Entity:{typeof(T_Entity).Name} index out off array:{index}");
            }

            var e = Get();
            Set(e, in _datas[index]);
            return e;
        }

        private void Init()
        {
            if (RelativePath == null || RelativePath.Length == 0 || RelativePath == "")
            {
                return;
            }
            _datas = new CsvReaderReflect<T_Data>()
                .Parse($"{Application.streamingAssetsPath}/ConstructorData/{RelativePath}");
        }
    }
}
