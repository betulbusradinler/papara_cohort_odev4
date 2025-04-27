using BookOperations.Common;
using BookOperations.DBOperations;

namespace BookOperations.BookOperations.GetBookById;
public class GetBooksByIdCommand
{
    public int Id { get; set; }

    private readonly BookStoreDbContext _bookStoreDbContext;
    public GetBooksByIdCommand(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;
    }

    public GetBooksByIdModel Handle()
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x => x.Id == Id);

        if (book is null)
            throw new InvalidOperationException("Böyle Bir Kitap Mevcut Değil");

        GetBooksByIdModel model = new GetBooksByIdModel
        {
            Title = book.Title,
            Genre = ((GenreEnum)book.GenreId).ToString(),
            PublishDate = book.PublishDate.Date.ToString("dd/MM/yy"),
            PageCount = book.PageCount
        };

        return model;

    }
}

public class GetBooksByIdModel
{
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
    public string Genre { get; set; }
}

