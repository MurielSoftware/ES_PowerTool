using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared.CSV
{
    public class CSVReader
    {
        public static CSVFile Read(string path)
        {
            CSVFile csvFile = new CSVFile();
            using (FileStream fileStream = File.OpenRead(path))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    LoadHeader(csvFile, streamReader);
                    LoadValues(csvFile, streamReader);
                }
            }
            return csvFile;
        }

        private static void LoadHeader(CSVFile file, StreamReader streamReader)
        {
            List<string> headerValues = ReadLine(streamReader);
            file.SetHeader(new CSVRow(headerValues));
        }

        private static void LoadValues(CSVFile file, StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                List<string> currentValues = ReadLine(streamReader);
                file.AddValue(new CSVRow(currentValues));
            }
        }

        private static List<string> ReadLine(StreamReader streamReader)
        {
            return Regex.Split(streamReader.ReadLine(), CSVFile.SEPARATOR).ToList();
        }
    }
}
