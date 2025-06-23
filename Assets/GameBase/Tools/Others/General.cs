using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    }
}
