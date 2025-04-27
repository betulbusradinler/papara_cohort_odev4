using BookOperations.DBOperations;

namespace BookOperations.BookOperations.DeleteBook;
public class DeleteBookCommand
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    public int BookId {get; set;}
    public DeleteBookCommand(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;
    }

    public void Handle()
    {
        var book = _bookStoreDbContext.Books.SingleOrDefault(x=>x.Id == BookId);
        if(book is null)
            throw new InvalidOperationException("Silinecek Kitap Bulunamadı!");
        _bookStoreDbContext.Books.Remove(book);
        _bookStoreDbContext.SaveChanges();
    }


}