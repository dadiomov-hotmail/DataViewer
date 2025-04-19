using System.Text;

namespace DataViewer
{
    public static class Parsing
    {
        public static double ParsePercent(string s)
        {
            double d;
            if (!double.TryParse(s.TrimEnd('%'), out d))
                return double.MinValue;

            if (d < -100 || d > 100)
                throw new Exception("Bad data");

            return d;
        }

        public static string FindLatestFile(string namePrefix, string extension, string dir)
        {
            // Enumerate files in the Downloads directory with the correct extension and names starting with the correct prefix 
            string[] files = System.IO.Directory.GetFiles(dir, $"{namePrefix}*.{extension}");

            // Find all files with the correct extension and name prefix in downloads directory
            if (files.Length == 0)
            {
                Console.WriteLine($"No files found in {dir} with extension {extension} and name starting with {namePrefix}");
                throw new InvalidDataException("No files found");
            }
            // Find creation times 
            DateTime[] creationTimes = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                creationTimes[i] = System.IO.File.GetCreationTime(files[i]);
            }

            // Find last modification times 
            DateTime[] modificationTimes = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                modificationTimes[i] = File.GetLastWriteTime(files[i]);
            }

            // Find the most recent file EXCLUDING modified files (want to use only originals)
            int mostRecentIndex = -1;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains("preprocessed"))
                    continue;

                if (mostRecentIndex < 0 || (creationTimes[i] > creationTimes[mostRecentIndex]))
                {
                    mostRecentIndex = i;
                }
            }

            if (mostRecentIndex < 0)
            {
                Console.WriteLine($"No UNMODIFIED files found in {dir} with extension {extension} and name starting with {namePrefix}");
                throw new InvalidDataException("No unmodified files found");
            }

            return files[mostRecentIndex];
        }

        public static string[][] ParseCsvFile(string f, bool dropSpecialChars)
        {
            if (string.IsNullOrEmpty(f))
                throw new InvalidOperationException("File for parsing is null");

            var lines = File.ReadAllLines(f);
            var parsedData = new string[lines.Length][];

            int j = 0;
            for (int i = 0; i < lines.Length; i++, j++)
            {
                var dataLine = lines[i];
                var data = ParseCsvLine(dataLine, dropSpecialChars);
                parsedData[j] = new string[data.Length];
                for (int k = 0; k < data.Length; k++)
                {
                    parsedData[j][k] = data[k];
                }
            }

            return parsedData;
        }


        public static string[] ParseCsvLine(string line, bool dropSpecialChars)
        {
            var result = new List<string>();
            var currentField = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                }
                else if (c == ',' && !insideQuotes)
                {
                    result.Add(currentField.ToString().Trim());
                    currentField.Clear();
                }
                else if (!dropSpecialChars)
                {
                    currentField.Append(c);
                }
                else if (c != ',' && c != '$' && c != '%')
                {
                    currentField.Append(c);
                }
            }

            result.Add(currentField.ToString().Trim());
            return result.ToArray();
        }
    }
}
