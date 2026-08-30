namespace TextAnalyzer;

using System.Text.RegularExpressions;

public static class Analyzer
{
    // Regex for letters, numbers and '
    private static readonly Regex WordRegex = new(@"\b[\p{L}\p{N}']+\b", RegexOptions.Compiled);

    public static int CountCharacters(string text) => text?.Length ?? 0;

    private static int CountCharactersNoSpaces(string text) => string.IsNullOrEmpty(text) ? 0 : text.Count(c => !char.IsWhiteSpace(c));

    private static int CountLetters(string text) => string.IsNullOrEmpty(text) ? 0 : text.Count(char.IsLetter);

    private static int CountDigits(string text) => string.IsNullOrEmpty(text) ? 0 : text.Count(char.IsDigit);

    private static int CountPunctuation(string text) => string.IsNullOrEmpty(text) ? 0 : text.Count(char.IsPunctuation);

    private static List<string> GetWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new List<string>();
        }

        return WordRegex.Matches(text).Select(m => m.Value).ToList();
    }

    public static int CountWords(string text) => GetWords(text).Count;

    public static int CountSentences(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return 0;
        }

        // Split into parts where . ! ? appears and then count things that are not empty
        var parts = Regex.Split(text, @"[\.!\?]+").Select(p => p.Trim()).Where(p => p.Length > 0);

        return parts.Count();
    }

    public static string FindMostCommonWord(string text)
    {
        var words = GetWords(text).Select(w => w.Trim().ToLowerInvariant()).Where(w => w.Length > 0);

        if (!words.Any())
        {
            return string.Empty;
        }

        return words.GroupBy(w => w).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).First().Key;
    }

    public static TextStatistics AnalyzeText(string text)
    {
        var stats = new TextStatistics
        {
            CharacterCount = CountCharacters(text),
            CharacterCountNoSpaces = CountCharactersNoSpaces(text),
            LetterCount = CountLetters(text),
            DigitCount = CountDigits(text),
            PunctuationCount = CountPunctuation(text)
        };

        var words = GetWords(text);
        stats.WordCount = words.Count;

        var normalized = words.Select(w => w.Trim().ToLowerInvariant()).Where(w => w.Length > 0).ToList();
        stats.UniqueWordCount = normalized.Distinct().Count();
        stats.MostCommonWord = normalized.Any() ? normalized.GroupBy(w => w).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).First().Key : string.Empty;

        if (normalized.Any())
        {
            stats.AverageWordLength = normalized.Average(w => w.Length);
            stats.LongestWord = normalized.OrderByDescending(w => w.Length).ThenBy(w => w).First();
            stats.ShortestWord = normalized.OrderBy(w => w.Length).ThenBy(w => w).First();
        }

        stats.SentenceCount = CountSentences(text);
        stats.AverageWordsPerSentence = stats.SentenceCount > 0 ? (double)stats.WordCount / stats.SentenceCount : 0.0;

        // Find the longest sentence by number of words
        if (!string.IsNullOrWhiteSpace(text))
        {
            var sentences = Regex.Split(text, @"[\.!\?]+").Select(s => s.Trim()).Where(s => s.Length > 0);
            var longestSentence = "";
            var maxWords = 0;
            foreach (var s in sentences)
            {
                var wc = GetWords(s).Count;
                if (wc > maxWords)
                {
                    maxWords = wc;
                    longestSentence = s;
                }
            }

            stats.LongestSentenceByWords = longestSentence;
        }

        return stats;
    }
}