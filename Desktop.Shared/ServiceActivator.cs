using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared
{
    public class ServiceActivator
    {
        private static Assembly serviceAssembly = Assembly.Load("ES_PowerTool.Data");

        public static T Get<T>()
        {
            Type instanceType = serviceAssembly.GetTypes().Where(x => x.Name == GetInstanceName(typeof(T).Name)).SingleOrDefault();
            return (T)Activator.CreateInstance(instanceType, new object[] { null });
        }

        private static string GetInstanceName(string interfaceName)
        {
            return interfaceName.Substring(1);
        }
    }
}
