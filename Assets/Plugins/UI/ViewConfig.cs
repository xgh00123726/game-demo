using NReco.Csv;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

namespace GameBase.UI
{
    public class ViewConfig
    {
        private static Color[] _colors;

        static ViewConfig()
        {
            Init("ViewRarityConfig.csv");
        }

        private static void Init(string relativePath)
        {
            if (relativePath == null || relativePath.Length == 0 || relativePath == "")
            {
                return;
            }
            StreamReader reader = File.OpenText($"{Application.streamingAssetsPath}/UI/{relativePath}");
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            int len = int.Parse(csvReader[0]);
            _colors = new Color[len];
            for (int i = 0; i < len; i++)
            {
                csvReader.Read();
                var r = int.Parse(csvReader[1].Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
                var g = int.Parse(csvReader[1].Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
                var b = int.Parse(csvReader[1].Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
                var a = int.Parse(csvReader[2], System.Globalization.NumberStyles.HexNumber) / 255f;

                _colors[i] = new Color(r, g, b, a);
            }

            reader.Close();
        }

        public static Color GetColor(int rarity)
        {
            return _colors[rarity];
        }
    }
}
