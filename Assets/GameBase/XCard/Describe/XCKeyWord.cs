using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GameBase.XCard
{
    public class XCKeyWord
    {
        private static JToken _parsedDict;
        static XCKeyWord()
        {
            string pathStr = File.ReadAllText($"{Application.streamingAssetsPath}/XCard/KeyWords.json", System.Text.Encoding.UTF8);
            _parsedDict = JObject.Parse(pathStr);
        }

        public static string ReadAttr(string keyword, string lang, string attr)
        {
            if (_parsedDict[keyword] == null)
            {
                Debug.LogWarning("illegal keyword");
                return "";
            }
            if (_parsedDict[keyword][lang] == null)
            {
                Debug.LogWarning("illegal lang");
                return "";
            }
            return _parsedDict[keyword][lang][attr].ToString();
        }

        public static string Tip(string keyword, string lang = "zh-cn")
        {
            return ReadAttr(keyword, lang, "tip");
        }

        public static string Content(string keyword, string lang = "zh-cn")
        {
            return ReadAttr(keyword, lang, "content");
        }
    }
}
