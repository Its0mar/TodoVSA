using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TodoVSA.Api.Data;

namespace TodoVSA.Api.Features.Todos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CreateTodoController(CreateTodoHandler createTodoHandler) : ControllerBase
{
    [HttpPost]
    public IActionResult Create([FromBody] CreateTodoCommand command)
    {
        var result = createTodoHandler.Handle(command).Result;
        if (!result.IsSuccess)
            return BadRequest("Todo already exists");
        
        return Ok(new {  result.Id });
    }
}


public record CreateTodoCommand(
    [Required]
    [StringLength(30, ErrorMessage = "Title cant exceed 30 chars")]
    string Title,

    [Required]
    [StringLength(150, ErrorMessage = "Description cant exceed 150 chars")]
    string Description);

public record CreateTodoResult(bool IsSuccess, int Id = -1);
public class CreateTodoHandler 
{
    public async Task<CreateTodoResult> Handle(CreateTodoCommand command)
    {
        var isDuplicate = AppDbContext.ToDos.Any(x => x.Title == command.Title && x.Description == command.Description);
        if (isDuplicate)
            return new CreateTodoResult(true);
        
        var task = new Todo()
        {
            Id = AppDbContext.ToDos.Count + 1,
            Title = command.Title,
            Description = command.Description,
            IsCompleted = false
        };
        AppDbContext.ToDos.Add(task);
        return new CreateTodoResult(true, task.Id);
    }
}