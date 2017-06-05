using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4N.Logger.Specific
{
    class FileLog : ISpecificLog
    {
        private StreamWriter _streamWriter;

        public FileLog(string file)
        {
            _streamWriter = new StreamWriter(file);
        }

        public void Dispose()
        {
            _streamWriter.Flush();
            _streamWriter.Close();
            _streamWriter.Dispose();
        }

        public void Write(string message)
        {
            _streamWriter.WriteLine(message);              
        }
    }
}
