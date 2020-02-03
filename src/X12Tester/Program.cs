using System;
using System.IO;
using X12Parser;

namespace X12Tester
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parse835Files(args);
            //Parse277File();

#if DEBUG
            if (!System.Diagnostics.Debugger.IsAttached) return;
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }
        private static void Parse277File()
        {
            const string fileName = @"C:\Dev\Projects\N091515.277";
            var segments = Parser.Parse(fileName);
            segments.ForEach(x => x.Dump($"{Environment.NewLine} *** {x.RecordType}  ***"));
        }

        private static void Parse835Files(string[] args)
        {
            var inputPath = @"C:\temp\835";
            var outputPath = @"C:\temp\835_parsed";
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
                var segments = Parser.Parse(file, factory);
                using (StreamWriter writer = File.CreateText(newFile))
                {
                    segments.ForEach(x => x.Dump(writer, $"{Environment.NewLine} *** {x.RecordType}  ***"));
                    writer.Flush();
                }
                break;
            }
            var end = DateTime.Now;
            var total = end - start;
            Console.WriteLine($"*** DONE {counter} files in {total} ***");
        }
    }
}
