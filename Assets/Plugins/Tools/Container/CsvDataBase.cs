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

    /// <summary>
    /// <list type="bullet">
    /// <item>csv的第一行第一列表示表格行数，同时第一行其它元素会被忽略</item>
    /// <item>csv除第一列外的每一列的第一个元素会被忽略，可以用于写上序号</item>
    /// </list>
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <typeparam name="T_DataBase">单例类型</typeparam>
    public abstract class CsvDataBase<T, T_DataBase> : Singleton<T_DataBase>
        where T : new()
        where T_DataBase : CsvDataBase<T, T_DataBase>, new()
    {
        protected T[] _datas;

        protected virtual string DataBasePath => null;
        protected string DefaultFolder(string path)
        {
            return $"{Application.streamingAssetsPath}/DataBases/{path}";
        }

        public CsvDataBase()
        {
            Init();
            Command.Register($"reload-database-{GetType().Name}", Init);
        }

        private void Init()
        {
            string path = DataBasePath;
            path ??= DefaultFolder($"{GetType().Name}.csv");

            _datas = new CsvReaderReflect<T>()
                .Parse(path);
        }

        public T this[int index] => _datas[index]; 
        public int Size => _datas.Length;
    }
}
