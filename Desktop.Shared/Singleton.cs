using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core
{
    public abstract class Singleton<T> where T : Singleton<T>
    {
        private static T instance;

        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = Activator.CreateInstance<T>();
                instance.Init();
            }
            return instance;
        }

        protected virtual void Init()
        {

        }
    }
}
