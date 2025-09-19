using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TodoVSA.Api.Data;

namespace TodoVSA.Api.Features.Todos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompleteTodoController(CompleteTodoHandler completeTodoHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Complete([FromBody] CompleteTodoCommand command)
    {
        var result = await completeTodoHandler.Handle(command);
        if (result) return Ok();
        return NotFound();
    }
}

public record CompleteTodoCommand(
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
    int Id);

public class CompleteTodoHandler
{
    public async Task<bool> Handle(CompleteTodoCommand command)
    {
        var todo = AppDbContext.ToDos.FirstOrDefault(x => x.Id == command.Id);

        if (todo is null) return false;
        
        todo.IsCompleted = true;
        return true;
    }
}