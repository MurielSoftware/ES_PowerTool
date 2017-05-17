using Desktop.Shared.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core
{
    public class ServiceActivator
    {
        private static Assembly serviceAssembly = Assembly.Load("ES_PowerTool.Data");

        //public static Dictionary<Type, Type> DTO_TO_SERVICE = CreateDtoToService();

        public static T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public static object Get(Type type)
        {
            Type instanceType = serviceAssembly.GetTypes().Where(x => x.Name == GetInstanceName(type.Name)).SingleOrDefault();
            return Activator.CreateInstance(instanceType, new object[] { Connection.GetInstance() });
        }

        private static string GetInstanceName(string interfaceName)
        {
            return interfaceName.Substring(1);
        }

        //private static Dictionary<Type, Type> CreateDtoToService()
        //{
        //    Dictionary<Type, Type> map = new Dictionary<Type, Type>();
        //    map.Add(typeof(FolderDto))
        //    return map;
        //}
    }
}
