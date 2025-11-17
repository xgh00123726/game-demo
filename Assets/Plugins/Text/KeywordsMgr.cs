using GameBase.Tools;
using NReco.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Texts
{
    public class KeywordsMgr : Singleton<KeywordsMgr>
    {
        private Dictionary<string, string> _keywords = new();
        internal string lang;
        internal string defaultLang;
        public string Lang
        {
            get => lang;
            set
            {
                if (defaultLang == null && value != null)
                {
                    defaultLang = value;
                }

                if (lang != value)
                {
                    lang = value;
                    LoadData();
                }
            }
        }

        public void LoadData(string file = null)
        {
            var fullPath = $"{Application.streamingAssetsPath}/Texts/{lang}/Keywords.csv";

            if (!File.Exists(fullPath))
            {
                fullPath = $"{Application.streamingAssetsPath}/Texts/{defaultLang}/Keywords.csv";
            }
            if (!File.Exists(fullPath))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log($"missing Keywords.csv in folder:{Application.streamingAssetsPath}/Texts/{defaultLang}");
            }

            using var reader = File.OpenText(fullPath);
            if (reader == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"there is no Keywords.csv in path: {Application.streamingAssetsPath}/Texts/{lang}");
                return;
            }
            _keywords.Clear();
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            while (csvReader.Read())
            {
                _keywords.Add(csvReader[0], csvReader[1]);
            }
        }

        public string ReplaceKeyword(string raw)
        {
            if (raw == "\\n")
            {
                return "\n";
            }

            if (raw.StartsWith("K.") || raw.StartsWith("k."))
            {
                var str = raw.Substring(2);
                if (str != null && str.Length > 0)
                {
                    return _keywords[raw.Substring(2)];
                }
            }

            return "";
        }

        public string ReplaceString(string raw)
        {
            string ret = "";

            bool meetPlaceHolder = false;
            int subStrLp = 0;
            int subStrLen = 0;

            int replaceStrLp = 0;
            int replaceStrLen = 0;

            for (int i = 0; i < raw.Length; i++)
            {
                char c = raw[i];

                if (!meetPlaceHolder)
                {
                    if (c == '{')
                    {
                        ret += raw.Substring(subStrLp, subStrLen);
                        subStrLen = 0;
                        replaceStrLp = i + 1;
                        replaceStrLen = 0;
                        meetPlaceHolder = true;
                    }
                    else
                    {
                        subStrLen++;
                    }
                }
                else if (meetPlaceHolder)
                {
                    if (c == '}')
                    {
                        ret += ReplaceKeyword(raw.Substring(replaceStrLp, replaceStrLen));
                        subStrLp = i + 1;
                        subStrLen = 0;
                        meetPlaceHolder = false;
                    }
                    else
                    {
                        replaceStrLen++;
                    }
                }
            }

            if (subStrLen > 0)
            {
                ret += raw.Substring(subStrLp, subStrLen);
            }

            return ret;
        }
    }
}
