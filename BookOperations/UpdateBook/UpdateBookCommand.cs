using BookOperations.DBOperations;

namespace BookOperations.BookOperations.UpdateBook;
public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    public int BookId { get; set; }

    private readonly BookStoreDbContext _bookStoreDbContext;
    public UpdateBookCommand(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;
    }

    public void Handle()
    {
        var book = _bookStoreDbContext
            .Books
            .SingleOrDefault(x => x.Id == BookId);

        if (Model.PageCount<=0)
            throw new InvalidDataException("Sayfa Sayısı 0 dan Küçük Olamaz");

        if (book is null)
            throw new InvalidDataException("Bu Kitap Mevcut Değil");

        book.Title = Model.Title;
        book.PublishDate = Model.PublishDate;
        book.GenreId = Model.GenreId;
        book.PageCount = Model.PageCount;

        _bookStoreDbContext.Books.Update(book);
        _bookStoreDbContext.SaveChanges();
    }

}

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}