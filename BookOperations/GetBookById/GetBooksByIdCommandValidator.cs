using FluentValidation;

namespace BookOperations.BookOperations.GetBookById;
public class GetBooksByIdCommandValidator:AbstractValidator<GetBooksByIdCommand>
{
    public int Id { get; set; }

    public GetBooksByIdCommandValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0);
    }

}
