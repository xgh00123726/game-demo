using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
namespace GameBase.XCard
{
    public class Settings
    {
        private static JToken _parsedDict;

        private static string _lang = "zh-cn";
        public static string Lang => _lang;
        static Settings()
        {
            Init();
        }

        private static void Init()
        {
            string pathStr = File.ReadAllText($"{Application.streamingAssetsPath}/XCard/settings.json", System.Text.Encoding.UTF8);
            _parsedDict = JObject.Parse(pathStr);

            var lang = _parsedDict["lang"];
            if (lang != null)
            {
                _lang = lang.ToString();
            }
        }
    }
}
