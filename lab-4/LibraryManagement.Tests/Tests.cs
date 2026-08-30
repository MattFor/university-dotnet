using Xunit;

namespace LibraryManagement.Tests;

public class LibraryTests
{
    [Fact]
    public void BorrowBook_ShouldMarkBookAsUnavailable()
    {
        var library = new Library();
        var book = new Book(1, "C# Programming", "John Doe");
        library.AddBook(book);

        library.BorrowBook(1, "Alice");

        Assert.False(book.IsAvailable);
        Assert.Equal("Alice", book.BorrowerName);
    }

    [Fact]
    public void BorrowBook_WhenBookIsAlreadyBorrowed_ShouldThrowException()
    {
        var library = new Library();
        library.AddBook(new Book(1, "C# Programming", "John Doe"));

        library.BorrowBook(1, "Alice");

        var ex = Assert.Throws<InvalidOperationException>(() => library.BorrowBook(1, "Bob"));

        Assert.Contains("already borrowed", ex.Message);
    }

    [Fact]
    public void ReturnBook_ShouldMarkBookAsAvailable()
    {
        var library = new Library();
        var book = new Book(1, "C# Programming", "John Doe");
        library.AddBook(book);

        library.BorrowBook(1, "Alice");
        library.ReturnBook(1);

        Assert.True(book.IsAvailable);
        Assert.Null(book.BorrowerName);
    }

    [Fact]
    public void ReturnBook_WhenBookWasNotBorrowed_ShouldThrowException()
    {
        var library = new Library();
        library.AddBook(new Book(1, "C# Programming", "John Doe"));

        var ex = Assert.Throws<InvalidOperationException>(() => library.ReturnBook(1));

        Assert.Contains("was not borrowed", ex.Message);
    }
}