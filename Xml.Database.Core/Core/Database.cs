using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xml.Database.Core
{
    public class Database
    {
        private string _path;

        public Database(string path)
        {
            _path = path;
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        public string GetPath()
        {
            return _path;
        }

        //public DatabaseContext Register(string name)
        //{
        //    return Register(name, true);
        //}

        //private DatabaseContext Register(string name, bool makeNew)
        //{
        //    return new DatabaseContext(Path, name, makeNew);
        //}
    }
}
