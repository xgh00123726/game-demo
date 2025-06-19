using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GameBase.XCard
{
    public class XCContent
    {
        private static string[] _contents;
        static XCContent()
        {
            Init();
        }

        private static void Init(string lang = "zh-cn")
        {
            _contents = File.ReadAllLines($"{Application.streamingAssetsPath}/XCard/lang/{Settings.Lang}.csv", System.Text.Encoding.UTF8);
        }

        public static string Get(int index)
        {
            if (index >= _contents.Length) return "";
            return _contents[index];
        }
    }
}
