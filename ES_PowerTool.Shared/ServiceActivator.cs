using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_PowerTool.Shared
{
    public class ServiceActivator
    {
        private static System.Reflection.Assembly assembly = System.Reflection.Assembly.Load("ES_PowerTool.Data");

        public T Get<T>()
        {
            Type type = assembly.GetTypes().Where(x => x.Name == GetInstanceName(typeof(T))).SingleOrDefault();
            return (T)Activator.CreateInstance(type, new object[] { null });
        }

        private static string GetInstanceName(Type type)
        {
            return type.Name.Substring(1);
        }
    }
}
