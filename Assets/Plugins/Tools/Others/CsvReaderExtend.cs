using NReco.Csv;
using System;
using System.IO;

namespace GameBase.Tools
{
    public class CsvReaderExtend
    {
        private string _path;
        private Action<int> _OnReadLen;
        private Action<int, CsvReader> _OnReadLine;

        public CsvReaderExtend(string path, Action<int> OnReadLen, Action<int, CsvReader> OnReadElem)
        {
            _path = path;
            _OnReadLen = OnReadLen;
            _OnReadLine = OnReadElem;
        }

        public void Parse()
        {
            if (_path == null || _path.Length == 0 || _path == "")
            {
                return;
            }
            StreamReader reader = File.OpenText(_path);
            CsvReader csvReader = new CsvReader(reader);
            csvReader.Read();
            if (int.TryParse(csvReader[0], out var len))
            {
                _OnReadLen?.Invoke(len);

                for (int i = 0; i < len; ++i)
                {
                    csvReader.Read();

                    _OnReadLine(i, csvReader);
                }
            }
            else
            {
                _OnReadLen?.Invoke(-1);

                int i = 0;
                while(csvReader.Read())
                {
                    _OnReadLine(i, csvReader);
                    i++;
                }
            }


            reader.Close();
        }
    }
}
