using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TodoVSA.Api.Data;

namespace TodoVSA.Api.Features.Todos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeleteTodoController(DeleteTodoHandler deleteTodoHandler) : ControllerBase
{
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteTodoCommand command)
    {
        var result = await deleteTodoHandler.Handle(command);
        if (result) return Ok();
        return NotFound();
    }
}

public record DeleteTodoCommand(
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0")]
    int Id);
public class DeleteTodoHandler
{
    public async Task<bool> Handle(DeleteTodoCommand command)
    {
        var todo = AppDbContext.ToDos.FirstOrDefault(x => x.Id == command.Id);
        if (todo != null)
        {
            AppDbContext.ToDos.Remove(todo);
            return true;
        }

        return false;
    }
}
