using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVFile
    {
        public const string SEPARATOR = ";";

        private Dictionary<string, string> _valueToColumn = new Dictionary<string, string>();
        private List<string> _headers = new List<string>();
        private List<CSVRow> _rows = new List<CSVRow>();

        public List<CSVRow> GetRows()
        {
            return _rows;
        }

        public CSVRow GetRow(int index)
        {
            return _rows[index];
        }

        public List<string> GetHeaders()
        {
            return _headers;
        }

        public string GetHeader(int index)
        {
            return _headers[index];
        }

        public void AddHeader(string header)
        {
            _headers.Add(header);
            _valueToColumn.Add(header, null);
        }

        public void AddRow(CSVRow csvRow)
        {
            _rows.Add(csvRow);
        }

        public Dictionary<string, string> GetRowToHeader(int index)
        {
            List<string> values = _rows[index].GetValues();
            int valueIndex = 0;
            foreach (string value in values)
            {
                string csvHeader = _headers[valueIndex++];
                _valueToColumn[csvHeader] = value;
            }
            return _valueToColumn;
        }

        public static CSVFile Load(string path)
        {
            CSVFile csvFile = new CSVFile();
            using (FileStream fileStream = File.OpenRead(path))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    List<string> headers = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
                    foreach (string header in headers)
                    {
                        csvFile.AddHeader(RemoveQuotesIfNeccessary(header));
                    }
                    while (!streamReader.EndOfStream)
                    {
                        List<string> values = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
                        List<string> valuesWithoutQuotes = new List<string>();
                        foreach (string value in values)
                        {
                            valuesWithoutQuotes.Add(RemoveQuotesIfNeccessary(value));
                        }
                        csvFile.AddRow(new CSVRow(valuesWithoutQuotes));
                    }
                }
            }
            return csvFile;
        }

        private static string RemoveQuotesIfNeccessary(string value)
        {
            if(value.StartsWith("\"") && value.EndsWith("\""))
            {
                string valueWithoutQuotes = value.Substring(1, value.Length - 2);
                return valueWithoutQuotes;
            }
            return value;
        }
    }
}
