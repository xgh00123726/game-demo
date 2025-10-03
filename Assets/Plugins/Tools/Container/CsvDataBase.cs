using UnityEngine;

namespace GameBase.Tools
{
    public static class CsvDataBasePath
    {
        public static string DefaultFolder(string path)
        {
            return $"{Application.streamingAssetsPath}/DataBases/{path}";
        }
    }
    public abstract class CsvDataBase<T, T_DataBase> : Singleton<T_DataBase>
        where T : new()
        where T_DataBase : CsvDataBase<T, T_DataBase>, new()
    {
        protected T[] _datas;

        protected abstract string DataBasePath { get; }

        public CsvDataBase()
        {
            Init();
            Command.Register($"reload-database-{GetType().Name}", Init);
        }

        private void Init()
        {
            _datas = new CsvReaderReflect<T>()
                .Parse(DataBasePath);
        }

        public T this[int index] => _datas[index]; 
        public int Size => _datas.Length;
    }
}
