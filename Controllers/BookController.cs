using BookOperations.BookOperations.CreateBook;
using BookOperations.BookOperations.DeleteBook;
using BookOperations.BookOperations.GetBookById;
using BookOperations.BookOperations.GetBooks;
using BookOperations.BookOperations.UpdateBook;
using BookOperations.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BookOperations.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;

    public BookController(BookStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new(_context);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        GetBooksByIdCommand command = new GetBooksByIdCommand(_context);
        try
        {
            command.Id = id;
            GetBooksByIdCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            var result = command.Handle();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context);
        try
        {
            command.Model = newBook;
            CreateBookCommandValidator validationRules = new CreateBookCommandValidator();
            validationRules.ValidateAndThrow(command);

            //ValidationResult result = validationRules.Validate(command); 
            // if(!result.IsValid)
            //     foreach(var item in result.Errors )
            //         Console.WriteLine("Özellik" + item.PropertyName + "- ErrorMessage: " + item.ErrorMessage);
            // else
            //     command.Handle();
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return StatusCode(201,"Created");
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateBookModel updateModel)
    {
        UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
        try
        {
            updateBookCommand.Id = id;
            updateBookCommand.Model = updateModel;
            UpdateBookCommandValidator validator = new();
            validator.ValidateAndThrow(updateBookCommand);
            updateBookCommand.Handle();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
        try
        {
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator validator = new();
            validator.ValidateAndThrow(deleteBookCommand);
            deleteBookCommand.Handle();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }
}
