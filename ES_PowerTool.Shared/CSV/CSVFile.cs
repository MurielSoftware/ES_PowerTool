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

        private List<CSVRow> _values = new List<CSVRow>();
        //private Dictionary<CSVValue, CSVValue> _headerToValue = new Dictionary<CSVValue, CSVValue>();
        //private List<CSVValue> _indexToHeader = new List<CSVValue>();
        CSVRow _header = new CSVRow();


        //public Dictionary<CSVValue, CSVValue> GetValuesToColumn(int rowIndex)
        //{
        //    CSVRow row = _values[rowIndex];
        //    //int valueIndex = 0;
        //    //foreach(KeyValuePair<CSVValue, CSVValue> valueToHeader in _header)
        //    //{
        //    //    valueToHeader.Value = row.GetValue(valueIndex++);
        //    //}
        //}

        public static CSVFile Load(string path)
        {
            CSVFile csvFile = new CSVFile();
            using (FileStream fileStream = File.OpenRead(path))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    LoadHeader(csvFile._header, streamReader);
                    LoadValues(csvFile._values, streamReader);
                    //List<string> headers = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
                    //foreach (string header in headers)
                    //{
                    //    csvFile.AddHeader(RemoveQuotesIfNeccessary(header));
                    //}
                    //while (!streamReader.EndOfStream)
                    //{
                    //    List<string> values = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
                    //    List<string> valuesWithoutQuotes = new List<string>();
                    //    foreach (string value in values)
                    //    {
                    //        valuesWithoutQuotes.Add(RemoveQuotesIfNeccessary(value));
                    //    }
                    //    csvFile.AddRow(new CSVRow(valuesWithoutQuotes));
                    //}
                }
            }
            return csvFile;
        }

        private static void LoadHeader(CSVRow header, StreamReader streamReader)
        {
            List<string> headerValues = ReadLine(streamReader);
            foreach(string headerValue in headerValues)
            {
                //_headerToValue
            }
            //headerValues.ForEach(x => header.Add(new CSVValue(x), null));
            //headers.ForEach(x => header.AddColumn(x));
        }

        private static void LoadValues(List<CSVRow> values, StreamReader streamReader)
        {
            while(!streamReader.EndOfStream)
            {
                List<string> currentValues = ReadLine(streamReader); ;
                values.Add(new CSVRow(currentValues));
                //currentValues = RemoveQuotesIfNeccessary(currentValues);
                //values.Add(new CSVRow(currentValues));
            }
        }

        private static List<string> ReadLine(StreamReader streamReader)
        {
            return Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
        }

      

        //private static List<string> RemoveQuotesIfNeccessary(List<string> values)
        //{
        //    List<string> valuesWithoutQuotes = new List<string>();
        //    foreach (string value in values)
        //    {
        //        valuesWithoutQuotes.Add(RemoveQuotesIfNeccessary(value));
        //    }
        //    return valuesWithoutQuotes;
        //}

        //private static string RemoveQuotesIfNeccessary(string value)
        //{
        //    if (value.StartsWith("\"") && value.EndsWith("\""))
        //    {
        //        string valueWithoutQuotes = value.Substring(1, value.Length - 2);
        //        return valueWithoutQuotes;
        //    }
        //    return value;
        //}

        //private Dictionary<string, string> _valueToColumn = new Dictionary<string, string>();
        //private List<string> _headers = new List<string>();
        //private List<CSVRow> _rows = new List<CSVRow>();

        //public List<CSVRow> GetRows()
        //{
        //    return _rows;
        //}

        //public CSVRow GetRow(int index)
        //{
        //    return _rows[index];
        //}

        //public List<string> GetHeaders()
        //{
        //    return _headers;
        //}

        //public string GetHeader(int index)
        //{
        //    return _headers[index];
        //}

        //public void AddHeader(string header)
        //{
        //    _headers.Add(header);
        //    _valueToColumn.Add(header, null);
        //}

        //public void AddRow(CSVRow csvRow)
        //{
        //    _rows.Add(csvRow);
        //}

        //public Dictionary<string, string> GetRowToHeader(int index)
        //{
        //    List<string> values = _rows[index].GetValues();
        //    int valueIndex = 0;
        //    foreach (string value in values)
        //    {
        //        string csvHeader = _headers[valueIndex++];
        //        _valueToColumn[csvHeader] = value;
        //    }
        //    return _valueToColumn;
        //}

        //public static CSVFile Load(string path)
        //{
        //    CSVFile csvFile = new CSVFile();
        //    using (FileStream fileStream = File.OpenRead(path))
        //    {
        //        using (StreamReader streamReader = new StreamReader(fileStream))
        //        {
        //            List<string> headers = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
        //            foreach (string header in headers)
        //            {
        //                csvFile.AddHeader(RemoveQuotesIfNeccessary(header));
        //            }
        //            while (!streamReader.EndOfStream)
        //            {
        //                List<string> values = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
        //                List<string> valuesWithoutQuotes = new List<string>();
        //                foreach (string value in values)
        //                {
        //                    valuesWithoutQuotes.Add(RemoveQuotesIfNeccessary(value));
        //                }
        //                csvFile.AddRow(new CSVRow(valuesWithoutQuotes));
        //            }
        //        }
        //    }
        //    return csvFile;
        //}

        //private static string RemoveQuotesIfNeccessary(string value)
        //{
        //    if(value.StartsWith("\"") && value.EndsWith("\""))
        //    {
        //        string valueWithoutQuotes = value.Substring(1, value.Length - 2);
        //        return valueWithoutQuotes;
        //    }
        //    return value;
        //}
    }
}
