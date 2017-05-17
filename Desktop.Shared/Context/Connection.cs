using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Context
{
    public class Connection : Singleton<Connection>
    {
        private const string CONNECTION_STRING = "Data Source={0}";
        private static Assembly serviceAssembly = Assembly.Load("Desktop.Data.Core");

        private string _databaseName;
        private IDatabaseContext _databaseContext;
        private IUnitOfWork _unitOfWork;

        public Connection()
        {
        }

        public void CreateConnection(string databaseName)
        {
            _databaseName = databaseName;

            Type modelContextType = serviceAssembly.GetTypes().Where(x => x.Name == "ModelContext").SingleOrDefault();
            Type unitOfWorkType = serviceAssembly.GetTypes().Where(x => x.Name == "UnitOfWork").SingleOrDefault();

            _databaseContext = (IDatabaseContext)Activator.CreateInstance(modelContextType, new object[] { GetConnectionString() });
            _unitOfWork = (IUnitOfWork)Activator.CreateInstance(unitOfWorkType, new object[] { _databaseContext });
        }

        public string GetConnectionString()
        {
            return string.Format(CONNECTION_STRING, _databaseName);
        }

        public IDatabaseContext GetContext()
        {
            return _databaseContext;
        }

        public void StartTransaction()
        {
            _unitOfWork.StartTransaction();
        }

        public void EndTransaction()
        {
            _unitOfWork.EndTransaction();
     //       _unitOfWork.Dispose();
        }

        
    }
}
