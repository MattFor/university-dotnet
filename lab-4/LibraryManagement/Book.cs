namespace LibraryManagement;

public class Book
{
    public int Id { get; }

    public string Title { get; }

    public string Author { get; }

    public bool IsAvailable { get; private set; } = true;

    public string? BorrowerName { get; private set; }

    public Book(int id, string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty!", nameof(title));
        }

        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author cannot be empty!", nameof(author));
        }

        Id = id;
        Title = title;
        Author = author;
    }

    public virtual string DisplayInfo()
    {
        return $"ID: {Id} -> Title: {Title} | Author: {Author} | Available: {IsAvailable}";
    }

    internal void Borrow(string borrowerName)
    {
        if (!IsAvailable)
        {
            throw new InvalidOperationException($"Book '{Title}' is already borrowed!");
        }

        if (string.IsNullOrWhiteSpace(borrowerName))
        {
            throw new ArgumentException("Borrower name cannot be empty!", nameof(borrowerName));
        }

        IsAvailable = false;
        BorrowerName = borrowerName;
    }

    internal void Return()
    {
        if (IsAvailable)
        {
            throw new InvalidOperationException($"Book '{Title}' was not borrowed!");
        }

        IsAvailable = true;
        BorrowerName = null;
    }
}