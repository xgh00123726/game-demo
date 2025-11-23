using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

        public static object CreateNotNullInstance(Type type)
        {
            var obj = CreateInstance(type);
            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }

            foreach (var field  in type.GetFields())
            {
                field.SetValue(obj, CreateNotNullInstance(field.FieldType));
            }
            foreach (var property in type.GetProperties())
            {
                if (!property.CanWrite || property.GetIndexParameters().Length > 0)
                {
                    continue;
                }
                property.SetValue(obj, CreateNotNullInstance(property.PropertyType));
            }

            return obj;
        }

        public static T CreateNotNullInstance<T>()
        {
            return (T)CreateNotNullInstance(typeof(T));
        }

        public static object CreateInstance(Type type)
        {
            if (type == typeof(string))
            {
                return "NNN";
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
