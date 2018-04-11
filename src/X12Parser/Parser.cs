using System;
using System.Collections.Generic;
using System.IO;

namespace X12Parser
{
    public static class Parser
    {
        public static List<X12> ParseText(string text, X12Factory factory)
        {
            var segments = text.Split(new[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<X12>();
            foreach (var segment in segments)
            {
                var obj = factory.GetX12Item(segment.Trim());
                list.Add(obj);
            }
            return list;
        }

        public static List<X12> ParseText(string text)
        {
            var factory = new X12Factory(typeof(X12));
            return ParseText(text, factory);
        }

        public static List<X12> Parse(string fileName, X12Factory factory)
        {
            var text = File.ReadAllText(fileName);
            return ParseText(text, factory);
        }

        public static List<X12> Parse(string fileName)
        {
            var factory = new X12Factory(typeof(X12));
            return Parse(fileName, factory);
        }
    }
}
