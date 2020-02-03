using System;
using System.Collections.Generic;
using System.IO;

namespace X12Parser
{
    public static class Parser
    {
        public static List<X12> ParseText(string text, X12Factory factory, bool dataChecks = true)
        {
            //var segments = text.Replace("\r\n", "").Replace("\n", "").Split(new[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
            var segments = text.Split(new[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<X12>();
            foreach (var segment in segments)
            {
                var obj = factory.GetX12Item(segment.Trim(), dataChecks);
                list.Add(obj);
            }
            return list;
        }

        public static List<X12> ParseText(string text, bool dataChecks = true)
        {
            var factory = new X12Factory(typeof(X12));
            return ParseText(text, factory, dataChecks);
        }

        public static List<X12> Parse(string fileName, X12Factory factory, bool dataChecks = true)
        {
            var text = File.ReadAllText(fileName);
            return ParseText(text, factory, dataChecks);
        }

        public static List<X12> Parse(string fileName, bool dataChecks = true)
        {
            var factory = new X12Factory(typeof(X12));
            return Parse(fileName, factory, dataChecks);
        }
    }
}
