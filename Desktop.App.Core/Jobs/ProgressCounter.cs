using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Jobs
{
    public class ProgressCounter
    {
        private int _targetObjectCount;
        private int _objectCount;

        public ProgressCounter(int targetObjectCount)
        {
            _targetObjectCount = targetObjectCount;
        }

        public void Increment()
        {
            _objectCount++;
        }
    }
}
