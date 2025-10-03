using NReco.Csv;
using System;
using System.IO;
using System.Reflection;

namespace GameBase.Tools
{
    public class CsvReaderReflect<T>
        where T : new()
    {
        private bool _isStruct;
        private FieldInfo[] _fieldInfos;

        public CsvReaderReflect()
        {
            if (typeof(T).IsClass)
            {
                _isStruct = false;
            }
            else
            {
                _isStruct = true;
            }

            _fieldInfos = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
        }

        public T[] Parse(string path)
        {
            T[] datas = null;
            StreamReader reader = File.OpenText(path);
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            if (int.TryParse(csvReader[0], out var len))
            {
                datas = new T[len];
                for (int i = 0; i < len; ++i)
                {
                    csvReader.Read();

                    if (_isStruct)
                    {
                        datas[i] = ParseStruct(_fieldInfos, csvReader);
                    }
                    else
                    {
                        datas[i] = ParseClass(_fieldInfos, csvReader);
                    }
                }
            }
            else
            {
                XLogger.Instance.Log("invalid csv style");
            }

            reader.Close();

            return datas;
        }

        private T ParseClass(FieldInfo[] fieldInfos, CsvReader csvReader)
        {
            var ret = new T();
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                var field = fieldInfos[i];
                var readerIndex = i + 1;
                if (field.FieldType.IsEnum)
                {
                    field.SetValue(ret, Enum.Parse(field.FieldType, csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(int))
                {
                    field.SetValue(ret, int.Parse(csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValue(ret, float.Parse(csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValue(ret, bool.Parse(csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValue(ret, csvReader[readerIndex]);
                }
                else
                {
                    XLogger.Instance.Log("not supported type");
                }
            }

            return ret;
        }

        private T ParseStruct(FieldInfo[] fieldInfos, CsvReader csvReader)
        {
            var ret = new T();
            TypedReference retRef = __makeref(ret);
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                var field = fieldInfos[i];
                var readerIndex = i + 1;
                if (field.FieldType.IsEnum)
                {
                    field.SetValueDirect(retRef, Enum.Parse(field.FieldType, csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(int))
                {
                    field.SetValueDirect(retRef, int.Parse(csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(float))
                {
                    field.SetValueDirect(retRef, float.Parse(csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(bool))
                {
                    field.SetValueDirect(retRef, bool.Parse(csvReader[readerIndex]));
                }
                else if (field.FieldType == typeof(string))
                {
                    field.SetValueDirect(retRef, csvReader[readerIndex]);
                }
                else
                {
                    XLogger.Instance.Log("not supported type");
                }
            }

            return ret;
        }
    }
}
