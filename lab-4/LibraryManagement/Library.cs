namespace LibraryManagement;

public class Library : IBookOperations
{
    private readonly List<Book> _books = new();

    public void AddBook(Book book)
    {
        if (book is null)
        {
            throw new ArgumentNullException(nameof(book));
        }

        if (_books.Any(b => b.Id == book.Id))
        {
            throw new InvalidOperationException($"Book with ID {book.Id} already exists!");
        }

        _books.Add(book);
    }

    public List<Book> ListAvailableBooks()
    {
        return _books.Where(book => book.IsAvailable).ToList();
    }

    public void BorrowBook(int bookId, string borrowerName)
    {
        var book = FindBookById(bookId);
        book.Borrow(borrowerName);
    }

    public void ReturnBook(int bookId)
    {
        var book = FindBookById(bookId);
        book.Return();
    }

    private Book FindBookById(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.Id == bookId);

        if (book is null)
        {
            throw new KeyNotFoundException($"Book with ID {bookId} was not found!");
        }

        return book;
    }
}