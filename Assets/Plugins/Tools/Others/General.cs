using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace GameBase.Tools
{
    public class General
    {
        public static int ToInt(string s)
        {
            return int.Parse(s);
        }
        // 获取文件夹内所有的文件名
        public static List<string> GetFileNamesInPaths(string path)
        {
            List<string> names = new List<string>();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            foreach (var f in files)
            {
                names.Add(f.Name);
            }
            return names;
        }

        public static void SetInstanceNotNull<T>(T obj)
        {
            SetInstanceNotNull(typeof(T), obj);
        }

        public static void SetInstanceNotNull(Type type, object obj)
        {
            var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = CreateInstance(field.FieldType);
                SetInstanceNotNull(field.FieldType, value);
                field.SetValue(obj, value);
            }
        }

        public static object CreateInstance(Type type)
        {
            if (type == typeof(string))
            {
                return null;
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
