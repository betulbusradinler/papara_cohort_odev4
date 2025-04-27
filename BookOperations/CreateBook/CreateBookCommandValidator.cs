using FluentValidation;
using Microsoft.AspNetCore.Rewrite;

namespace BookOperations.BookOperations.CreateBook;
public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
{
    public CreateBookModel Model { get; set; }
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}
