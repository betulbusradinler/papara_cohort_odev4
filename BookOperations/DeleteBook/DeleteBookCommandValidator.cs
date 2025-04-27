using FluentValidation;

namespace BookOperations.BookOperations.DeleteBook;
public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
{
    public int BookId {get; set;}
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId).GreaterThan(0);
    }

}