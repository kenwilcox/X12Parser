using System;
using System.Linq;
using System.IO;
using X12Parser;
using System.Collections.Generic;

namespace X12Tester
{
    public static class Program
    {
        public static void Main()//string[] args)
        {
            Parse835Files();
            Parse277File();
            ParsePLBFiles();

#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached) return;
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }
        private static void Parse277File()
        {
            var fileName = TesterSettings.Test277;
            var segments = Parser.Parse(fileName);
            segments.ForEach(x => x.Dump($"{Environment.NewLine} *** {x.RecordType}  ***"));
        }

        private static void ParsePLBFiles()
        {
            var items = File.ReadAllLines(TesterSettings.ProcessCsv).Skip(1);
            var factory = new X12Factory(typeof(X12));
            var outputPath = TesterSettings.PlbOutput;
            var max = 0;
            var dates = new List<string>();

            foreach (var item in items)
            {
                var parts = item.Split(',');
                var file = parts[0];
                var count = int.Parse(parts[1]);

                var payer = file.Split('\\')[5];
                var newPath = Path.Combine(outputPath, payer);
                var newFile = file.Replace(Path.GetDirectoryName(file), newPath) + ".txt";
                Console.WriteLine($"{file} - {newFile}");

                var segments = Parser.Parse(file, factory, false);
                var plbs = segments.Where(x => x.RecordType == "PLB").ToList();
                if (plbs.Count != count) throw new Exception("Didn't get what we expected");

                //Directory.CreateDirectory(newPath);
                //using (StreamWriter writer = File.CreateText(newFile))
                //{
                //    //segments.ForEach(x => x.Dump(writer, $"{Environment.NewLine} *** {x.RecordType}  ***"));
                //    //segments.Where(x => x.RecordType == "PLB" || x.RecordType == "TRN" || x.RecordType == "BPR").ToList().ForEach(x => x.Dump(writer, $"{Environment.NewLine} *** {x.RecordType}  ***"));
                //    segments.Where(x => x.RecordType == "PLB").ToList().ForEach(x => x.Dump(writer, $"{Environment.NewLine} *** {x.RecordType}  ***"));
                //    writer.Flush();
                //}

                // find max - testing custom properties
                // if (plb is PLB p) - borowed from other languages? Swift?
                foreach (var plb in plbs.Where(x => x is PLB).Select(x => x as PLB))
                {
                    var pMax = plb.ReferenceIdentification.Length;
                    if (pMax > max) max = pMax;

                    if (!plb.Date.EndsWith("1231")) dates.Add(plb.Date);
                    if (plb.AdjustmentReasonCode1.Length < 2)
                    {
                        Console.WriteLine("Stop");
                    }

                    //foreach(var section in plb.Sections)
                    //{
                    //    Console.WriteLine(section);
                    //}
                }
            }

            Console.WriteLine($"Max = {max}");
            Console.WriteLine($"Date Count: {dates.Count}");
            Console.WriteLine($"Date Count: {dates.Distinct().Count()}");
        }

        private static void Parse835Files()
        {
            var inputPath = TesterSettings.InputPath;
            var outputPath = Path.Combine(inputPath, "_parsed");
            Directory.CreateDirectory(outputPath);
            var start = DateTime.Now;
            var counter = 0;

            // The factory caches all the objects it's going to use.
            // If you pass in the factory it's all cached
            // If you don't it creates a factory for you, which
            // rebuilds the cache every time
            var factory = new X12Factory(typeof(X12));
            foreach (var file in Directory.EnumerateFiles(inputPath))
            {
                counter++;
                var newFile = file.Replace(inputPath, outputPath);
                newFile = Path.ChangeExtension(newFile, "txt");
                var segments = Parser.Parse(file, factory, false);
                using (StreamWriter writer = File.CreateText(newFile))
                {
                    segments.ForEach(x => x.Dump(writer, $"{Environment.NewLine} *** {x.RecordType}  ***"));
                    writer.Flush();
                }
                //break;
            }
            var end = DateTime.Now;
            var total = end - start;
            var dummy = factory.GetPropertiesForType("ISA");
            Console.WriteLine($"ISA has {dummy.Count()} properties");
            Console.WriteLine($"*** DONE {counter} files in {total} ***");
        }
    }
}
