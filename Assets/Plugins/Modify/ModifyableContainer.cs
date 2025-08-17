using GameBase.Tools;
using NReco.Csv;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyableContainer<T> : IDContainer<Modifyable<T>, ModifyableContainer<T>>
    {
        public static string ValidLabel => "-";

        public static int GetIDOfKey(string key)
        {
            return _nameIDDict[key];
        }

        public bool AddModify(string key, Modifyer<T> modifyer)
        {
            if (!ContainsKey(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
                return false;
            }

            this[key].AddModify(modifyer);
            return true;
        }

        public bool AddModify(int key, Modifyer<T> modifyer)
        {
            if (!ContainsValueWith(key))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"trying modify a unexist modifyable value which key is {key}");
                return false;
            }

            this[key].AddModify(modifyer);
            return true;
        }

        protected override void GetNameIDDict(out Dictionary<string, int> dict)
        {
            dict = new Dictionary<string, int>();
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/public/ModifyTable_{typeof(T).Name}.csv");
            CsvReader csvReader = new CsvReader(reader);

            while (csvReader.Read())
            {
                string label = csvReader[0];
                if (label != "-")
                {
                    continue;
                }

                dict[csvReader[2]] = int.Parse(csvReader[1]);
            }

            reader.Close();
        }
    }
}
