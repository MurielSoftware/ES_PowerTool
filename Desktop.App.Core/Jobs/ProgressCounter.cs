using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.App.Core.Jobs
{
    public class ProgressCounter
    {
        private int _title;
        private string _message;
        private int _targetObjectCount;
        private int _objectCount;

        public ProgressCounter(int title, string initialMessage, int targetObjectCount)
        {
            _title = title;
            _targetObjectCount = targetObjectCount;
            _message = initialMessage;
        }

        public void NextStep(string message)
        {
            _message = message; 
        }

        public void Increment()
        {
            _objectCount++;
        }
    }
}
