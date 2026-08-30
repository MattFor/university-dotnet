using LibraryManagementREST.Data;
using LibraryManagementREST.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BooksDbContext>(options => options.UseSqlite("Data Source=books.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BooksDbContext>();
    db.Database.EnsureCreated();
}

// GET /api/books
app.MapGet("/api/books", async (BooksDbContext db) =>
{
    var books = await db.Books.AsNoTracking().ToListAsync();
    return Results.Ok(books);
});

// GET /api/books/{ID}
app.MapGet("/api/books/{id:int}", async (int id, BooksDbContext db) =>
{
    var book = await db.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    return book is null ? Results.NotFound() : Results.Ok(book);
});

// POST /api/books/
app.MapPost("/api/books", async (Book book, BooksDbContext db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();

    return Results.Created($"/api/books/{book.Id}", book);
});

// PUT /api/books/{ID}
app.MapPut("/api/books/{id:int}", async (int id, Book input, BooksDbContext db) =>
{
    var book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound();
    }

    book.Title = input.Title;
    book.Author = input.Author;
    book.PublishedYear = input.PublishedYear;
    book.IsRead = input.IsRead;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// DELETE /api/books/{ID}
app.MapDelete("/api/books/{id:int}", async (int id, BooksDbContext db) =>
{
    var book = await db.Books.FirstOrDefaultAsync(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound();
    }

    db.Books.Remove(book);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();