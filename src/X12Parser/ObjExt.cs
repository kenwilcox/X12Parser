using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace X12Parser
{
    public static class ObjExt
    {
        public static void Dump(this object obj, string title = "")
        {
            Dump(obj, Console.Out, title);
        }

        public static void Dump(this object obj, TextWriter writer, string title = "")
        {
            var type = obj.GetType();
            var props = type.GetProperties();
            if (!string.IsNullOrWhiteSpace(title)) writer.WriteLine($"{title}"); //*** {title} ***
            foreach (var prop in props)
            {
                var val = prop.GetValue(obj);
                //if (!(val is ValueType)) Dump(val, title);
                writer.WriteLine($"{prop.Name} = {StrVal(val)}");
            }
        }

        private static string StrVal(object o)
        {
            switch (o)
            {
                case null:
                    return "null";
                case DateTime time:
                    return time.ToShortDateString();
                case ValueType _:
                case string _:
                    return o.ToString();
                case List<object> enumerable:
                    return "[" + string.Join(",", enumerable.Select(x => x.ToString())) + "]";
                case IEnumerable<object> enumerable:
                    return "[" + string.Join(",", enumerable.Select(x => x.ToString())) + "]";
                case IEnumerable enumerable:
                    var ret = "";
                    foreach (var x in enumerable)
                    {
                        ret += x.ToString() + ",";
                    }
                    return "[" + ret.TrimEnd(',') + "]";
            }

            return "{}";
        }
    }
}
