using NReco.Csv;
using System.IO;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;

namespace GameBase.Tools
{
    public class CsvWriterExtend
    {
        public static void Write<T>(T[] datas, string path)
        {
            using var fileWriter = new StreamWriter(path);
            using var csvWriter = new CsvHelper.CsvWriter(fileWriter, 
                new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(datas);
        }
    }
}
