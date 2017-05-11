using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.DataTypes
{
    public class FilePath
    {
        public string Path { get; set; }

        public FilePath(string path)
        {
            Path = path;
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
