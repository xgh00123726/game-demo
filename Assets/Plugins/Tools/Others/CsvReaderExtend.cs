using CsvHelper.Configuration;
using NReco.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace GameBase.Tools
{
    public class CsvReaderExtend
    {
        public static List<T> Read<T>(string path)
        {
            using var fileReader = new StreamReader(path);
            using var csvReader = new CsvHelper.CsvReader(fileReader,
                System.Globalization.CultureInfo.InvariantCulture);
            List<T> ret = new();
            foreach (var item in csvReader.GetRecords<T>()) 
            {
                ret.Add(item);
            }

            return ret;
        }
    }
}
