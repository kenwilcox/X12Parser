using System;
using System.IO;

namespace X12Parser
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //if (args.Length < 1) return;
            //var fileName = args[0];
            //if (!File.Exists(fileName)) return;
            //const string fileName = @"C:\Users\wilcoxk\Documents\EraSummary\508502268_ERA_835_5010_20180329.835.pretty";
            //const string fileName = @"C:\Users\wilcoxk\Documents\EraSummary\508502268_ERA_835_5010_20180329.835";
            //const string fileName = @"C:\Users\wilcoxk\Documents\EraSummary\CEP GALEN 032118.835";
            //const string fileName = @"C:\Users\wilcoxk\Documents\EraSummary\RCEP GALEN 032218032118.835.txt";
            //const string fileName = @"C:\Users\wilcoxk\Documents\EraSummary\RCEP GALEN 032218032118.835.trunc.txt";
            const string fileName = @"C:\Dev\Projects\N091515.277";
            var segments = Parser.Parse(fileName);
            // So do something with the segments...
            segments.ForEach(x => x.Dump($"{Environment.NewLine} *** {x.RecordType}  ***"));
#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached) return;
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }
    }
}
