namespace TextAnalyzer.Tests;

using TextAnalyzer;
using NUnit.Framework;

[TestFixture]
public class TextAnalyzerTests
{
    [Test]
    public void CountCharacters_ShouldReturnCorrectNumber()
    {
        const string text = "Hello, world!";
        int result = Analyzer.CountCharacters(text);
        Assert.AreEqual(13, result);
    }

    [Test]
    public void CountWords_ShouldReturnCorrectNumber()
    {
        const string text = "Hello world!";
        int result = Analyzer.CountWords(text);
        Assert.AreEqual(2, result);
    }

    [Test]
    public void CountSentences_ShouldReturnCorrectNumber()
    {
        const string text = "Hello world! How are you? Amogus.";
        int result = Analyzer.CountSentences(text);
        Assert.AreEqual(3, result);
    }

    [Test]
    public void MostCommonWord_ShouldReturnCorrectWord()
    {
        const string text = "apple banana apple orange apple banana mango bongo";
        string result = Analyzer.FindMostCommonWord(text);
        Assert.AreEqual("apple", result);
    }

    [Test]
    public void AnalyzeText_WithEmptyString_ShouldReturnZeroes()
    {
        const string text = "";
        var result = Analyzer.AnalyzeText(text);

        Assert.AreEqual(0, result.CharacterCount);
        Assert.AreEqual(0, result.WordCount);
        Assert.AreEqual(0, result.SentenceCount);
    }
}