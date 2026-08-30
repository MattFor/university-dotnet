namespace TextAnalyzer;

public class TextStatistics
{
    public int CharacterCount { get; set; }
    public int CharacterCountNoSpaces { get; set; }
    public int LetterCount { get; set; }
    public int DigitCount { get; set; }
    public int PunctuationCount { get; set; }
    public int WordCount { get; set; }
    public int UniqueWordCount { get; set; }
    public string MostCommonWord { get; set; } = string.Empty;
    public double AverageWordLength { get; set; }
    public string LongestWord { get; set; } = string.Empty;
    public string ShortestWord { get; set; } = string.Empty;
    public int SentenceCount { get; set; }
    public double AverageWordsPerSentence { get; set; }
    public string LongestSentenceByWords { get; set; } = string.Empty;
}