using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.Jobs
{
    public class ProgressCounter : PropertyChangedProvider
    {
        private string _message;


        public string Title { get; set; }
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(() => Message); }
        }
        private int _targetObjectCount;
        private int _objectCount;

        public ProgressCounter(string title, string initialMessage, int targetObjectCount)
        {
            Title = title;
            Message = initialMessage;
            _targetObjectCount = targetObjectCount;            
        }

        public void NextStep(string message)
        {
            Message = message; 
        }

        public void Increment()
        {
            _objectCount++;
        }
    }
}
