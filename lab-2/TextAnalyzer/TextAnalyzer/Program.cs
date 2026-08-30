namespace TextAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        string text = "";

        if (args.Length > 0 && File.Exists(args[0]))
        {
            // File path
            try
            {
                text = File.ReadAllText(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("File read error: " + ex.Message);
                return;
            }
        }
        else
        {
            Console.WriteLine("Choose input source:");
            Console.WriteLine("1. By hand");
            Console.WriteLine("2. File path");
            Console.Write("Which one? (1 or 2): ");
            var choice = Console.ReadLine();

            if (choice == "2")
            {
                Console.Write("Input file path: ");
                var path = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    Console.WriteLine("Invalid file path!");
                    return;
                }

                try
                {
                    text = File.ReadAllText(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error reading file: " + ex.Message);
                    return;
                }
            }
            else
            {
                Console.WriteLine("Input text (end with an empty line):");
                string line;
                var lines = new System.Text.StringBuilder();
                while (true)
                {
                    line = Console.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    if (line == "")
                    {
                        break;
                    }

                    lines.AppendLine(line);
                }

                text = lines.ToString();
            }
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("No text to analyze.");
            return;
        }

        var stats = Analyzer.AnalyzeText(text);
        PrintStats(stats);
    }

    static void PrintStats(TextStatistics s)
    {
        Console.WriteLine("\n--- Results ---");
        Console.WriteLine($"Chars (with spaces): {s.CharacterCount}");
        Console.WriteLine($"Chars (no spaces): {s.CharacterCountNoSpaces}");
        Console.WriteLine($"Letters: {s.LetterCount}");
        Console.WriteLine($"Numbers: {s.DigitCount}");
        Console.WriteLine($"Chars like , . ; etc.: {s.PunctuationCount}");
        Console.WriteLine($"Words: {s.WordCount}");
        Console.WriteLine($"Unique words: {s.UniqueWordCount}");
        Console.WriteLine($"Most common word: {s.MostCommonWord}");
        Console.WriteLine($"Avg. word length: {s.AverageWordLength:F2}");
        Console.WriteLine($"Longest word: {s.LongestWord}");
        Console.WriteLine($"Shortest word: {s.ShortestWord}");
        Console.WriteLine($"Number of sentences: {s.SentenceCount}");
        Console.WriteLine($"Avg. word count per sentence: {s.AverageWordsPerSentence:F2}");
        Console.WriteLine($"Longest sentence (by word count): {s.LongestSentenceByWords}");
    }
}