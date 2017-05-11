using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ES_PowerTool.CSV
{
    public class CSVLoader
    {
        public const string SEPARATOR = ";";

        public void Load(string path)
        {
            CSVFile csvFile = new CSVFile();
            using (FileStream fileStream = File.OpenRead(path))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    int columnIndex = 0;
                    List<string> headers = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
                    foreach(string header in headers)
                    {
                        csvFile.AddHeader(new CSVHeader(header, columnIndex++));
                    }
                    while(!streamReader.EndOfStream)
                    {
                        columnIndex = 0;
                        List<string> values = Regex.Split(streamReader.ReadLine(), SEPARATOR).ToList();
                        csvFile.AddRow(new CSVRow(values));
                    }
                }
            }
        }
    }
}
