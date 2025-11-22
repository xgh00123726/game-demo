using NReco.Csv;
using System.IO;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;

namespace GameBase.Tools
{
    public class CsvWriterReflect<T>
    {
        private FieldInfo[] _fieldInfos;

        public CsvWriterReflect()
        {
            _fieldInfos = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
        }

        private string GetTableHead(int length, FieldInfo[] fieldInfos)
        {
            var ret = $"{length}";
            foreach (var info in fieldInfos)
            {
                ret += $", {info.Name}";
            }

            return ret;
        }

        public void Write(T[] datas, string path)
        {
            using var fileWriter = new StreamWriter(path);
            using var csvWriter = new CsvHelper.CsvWriter(fileWriter, 
                new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(datas);
        }
    }
}
