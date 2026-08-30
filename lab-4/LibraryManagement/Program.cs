using LibraryManagement;

var library = new Library();

library.AddBook(new Book(1, "C# Programming", "John Doe"));
library.AddBook(new Book(2, "Design Patterns", "Gamma et al."));
library.AddBook(new EBook(3, "Clean Code", "Robert C. Martin", "PDF"));

Console.WriteLine("Available books:");
foreach (var book in library.ListAvailableBooks())
{
    Console.WriteLine(book.DisplayInfo());
}

try
{
    library.BorrowBook(1, "Alice");
    Console.WriteLine("Book borrowed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

try
{
    library.ReturnBook(1);
    Console.WriteLine("Book returned successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Available books after operations:");
foreach (var book in library.ListAvailableBooks())
{
    Console.WriteLine(book.DisplayInfo());
}