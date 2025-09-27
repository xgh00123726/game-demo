using NReco.Csv;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Texts
{
    public partial class TextMgr
    {
        public static string languagePath = "zh-cn";
        private static string[] keywords;
        private static string[] commons;
        private static Dictionary<string, string[]> _dataTexts = new();

        static TextMgr()
        {
            Init();
        }

        private static void WriteTo(string fileName, out string[] buffer)
        {
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/Texts/{languagePath}/{fileName}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            buffer = new string[int.Parse(csvReader[0])];
            for (int i = 0; i < buffer.Length; i++)
            {
                csvReader.Read();
                buffer[i] = csvReader[1];
            }

            reader.Close();
        }

        private static void Init()
        {
            if (languagePath == null || languagePath.Length == 0 || languagePath == "")
            {
                return;
            }
            WriteTo("common.csv", out commons);
            WriteTo("keywords.csv", out keywords);
            ExtendInit();
        }



        public static void Init(string path)
        {
            WriteTo(path, out string[] buffer);
            _dataTexts[path] = buffer;
        }

        private static string GetReplaceString(char tag, int index, string[] args)
        {
            // 从keywords中替换
            if (tag == 'k')
            {
                if (index >= keywords.Length)
                {
                    return "";
                }

                return keywords[index];
            }

            // 从输入中替换
            if (tag == 'v')
            {
                if (index >= args.Length)
                {
                    return "";
                }

                return args[index];
            }

            return "";
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
            char replaceTag = ' ';
            int replaceIndex = 0;
            int subStrLp = 0;
            int subStrLen = 0;

            for(int i = 0; i < val.Length; i++)
            {
                char c = val[i];
                if (!meetPlaceHolder)
                {
                    if (c == '{')
                    {
                        ret += val.Substring(subStrLp, subStrLen);
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
                        ret += GetReplaceString(replaceTag, replaceIndex, args);
                        replaceTag = ' ';
                        replaceIndex = 0;
                        meetPlaceHolder = false;
                        subStrLp = i + 1;
                        subStrLen = 0;
                    }
                    else if (replaceTag == ' ')
                    {
                        replaceTag = c;
                    }
                    else if (replaceTag != ' ')
                    {
                        replaceIndex = replaceIndex * 10 + c - '0';
                    }
                }
            }

            return ret;
        }

        public static string Get(int index) => commons[index];

        public static string GetKeyword(int index) => keywords[index];
    }
}
