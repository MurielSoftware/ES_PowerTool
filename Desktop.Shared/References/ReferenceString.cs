﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Shared.Core.References
{
    public class ReferenceString
    {
        private string _value;

        public ReferenceString(string value)
        {
            _value = value;
        }

        public ReferenceString(Guid id, string value)
        {
            _value = id.ToString() + ":" + value + ";";
        }

        public Guid GetId()
        {
            Dictionary<Guid, string> parsedReferenceString = Parse(_value);
            return parsedReferenceString.First().Key;
        }

        public List<Guid> GetIds()
        {
            Dictionary<Guid, string> parsedReferenceString = Parse(_value);
            return parsedReferenceString.Keys.ToList();
        }

        public string GetValue()
        {
            Dictionary<Guid, string> parsedReferenceString = Parse(_value);
            return parsedReferenceString.First().Value;
        }

        public void Append(Guid id, string value)
        {
            _value += id.ToString() + ":" + value + ";";
        }

        public override string ToString()
        {
            return _value;
        }

        public static Dictionary<Guid, string> Parse(string referenceString)
        {
            string[] values = referenceString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<Guid, string> parsedReferenceString = new Dictionary<Guid, string>();
            foreach (string value in values)
            {
                string[] pair = value.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                parsedReferenceString.Add(Guid.Parse(pair[0]), pair[1]);
            }
            return parsedReferenceString;
        }
    }
}
