using NReco.Csv;
using System;
using System.IO;
using System.Reflection;

namespace GameBase.Tools
{
    public class CsvReaderReflect<T>
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

            if (!File.Exists(path))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"invalid file path:{path}");
            }

            StreamReader reader = File.OpenText(path);

            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            if (int.TryParse(csvReader[0], out var len))
            {
                datas = new T[len];
                for (int i = 0; i < len; ++i)
                {
                    var ret = csvReader.Read();
                    if (!ret)
                    {
                        XLogger.Instance.Level(XLogger.LogLevel.Error)
                            .Log($"table{path}'s line num is invalid, may cast null reference");
                        break;
                    }

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
            var ret = Activator.CreateInstance<T>();
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                var field = fieldInfos[i];
                var readerIndex = i + 1;
                if (field.FieldType.IsEnum)
                {
                    var enumParse = Enum.TryParse(field.FieldType, csvReader[readerIndex], out var val);
                    if (!enumParse)
                    {
                        XLogger.Instance.Level(XLogger.LogLevel.Error)
                            .Log($"{csvReader[readerIndex]} is not a valid enum");
                        return ret;
                    }
                    field.SetValue(ret, val);
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
            var ret = Activator.CreateInstance<T>();
            TypedReference retRef = __makeref(ret);
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                var field = fieldInfos[i];
                var readerIndex = i + 1;
                if (field.FieldType.IsEnum)
                {
                    var enumParse = Enum.TryParse(field.FieldType, csvReader[readerIndex], out var val);
                    if (!enumParse)
                    {
                        XLogger.Instance.Level(XLogger.LogLevel.Error)
                            .Log($"{csvReader[readerIndex]} is not a valid enum");
                        return ret;
                    }
                    field.SetValueDirect(retRef, val);
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
