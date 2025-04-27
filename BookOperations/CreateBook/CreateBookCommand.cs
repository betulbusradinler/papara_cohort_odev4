using System.ComponentModel.DataAnnotations;
using BookOperations.DBOperations;

namespace BookOperations.BookOperations.CreateBook;
public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext bookStoreDbContext;
    public CreateBookCommand(BookStoreDbContext bookStoreDbContext)
    {
        this.bookStoreDbContext = bookStoreDbContext;
    }

    public void Handle()
    {
        var book = bookStoreDbContext
            .Books
            .SingleOrDefault(x => x.Title == Model.Title);
        if (book is not null)
            throw new InvalidOperationException("Kitap zaten mevcut");
        book = new Book
        {
            Title = Model.Title,
            PublishDate = Model.PublishDate,
            PageCount = Model.PageCount,
            GenreId = Model.GenreId
        };

        bookStoreDbContext.Books.Add(book);
        bookStoreDbContext.SaveChanges();
    }

}

public class CreateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}