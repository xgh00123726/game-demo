using GameBase.Tools;
using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Texts
{
    internal enum ParseState
    {
        Normal,
        MeetPlaceHolder,
        MeetKeywordsFirstK,
        MeetKeywordsKDot,
        ParsingKeywordStr,

    }

    public partial class TextMgr
    {
        public static string languagePath = "zh-cn";
        private static Dictionary<string, string> keywords = new();
        private static Dictionary<string, string[]> _dataTexts = new();

        static TextMgr()
        {
            Init();
        }

        private static string[] GetStrings(string fileName)
        {
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/Texts/{languagePath}/{fileName}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            string[] buffer = new string[int.Parse(csvReader[0])];
            for (int i = 0; i < buffer.Length; i++)
            {
                csvReader.Read();
                buffer[i] = csvReader[1];
            }

            reader.Close();
            return buffer;
        }

        private static void Init()
        {
            if (languagePath == null || languagePath.Length == 0 || languagePath == "")
            {
                return;
            }
            InitKeywords();
            ExtendInit();
        }

        private static void InitKeywords()
        {
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/Texts/Keywords.csv");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            while (csvReader.Read())
            {
                keywords.Add(csvReader[0], csvReader[1]);
            }
            reader.Close();
        }

        private static string GetReplaceString(string raw)
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
                    return keywords[raw.Substring(2)];
                }
            }


            return "";
        }


        public static void InitFile(string path)
        {
            _dataTexts[path] = GetStrings(path);
        }


        public static string Get(string path, int index, params string[] args)
        {
            if (index >= _dataTexts[path].Length)
            {
                return "NNN";
            }

            string val = _dataTexts[path][index];
            string ret = "";

            bool meetPlaceHolder = false;
            int subStrLp = 0;
            int subStrLen = 0;

            int replaceStrLp = 0;
            int replaceStrLen = 0;

            for(int i = 0; i < val.Length; i++)
            {
                char c = val[i];

                if (!meetPlaceHolder)
                {
                    if (c == '{')
                    {
                        ret += val.Substring(subStrLp, subStrLen);
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
                        ret += GetReplaceString(val.Substring(replaceStrLp, replaceStrLen));
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

            return ret;
        }
    }
}
