using Microsoft.AspNetCore.Mvc;
using TodoVSA.Api.Features.Todos.Models;

namespace TodoVSA.Api.Features.Todos.Controllers;

[ApiController, Route("api/[controller]")]
public class GetTodosController(GetTodosHandler handler) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var todos = handler.Handle();
        return Ok(todos);
    }
}

public class GetTodosHandler
{
    public  IReadOnlyList<Todo> Handle()
    {
        return Data.AppDbContext.ToDos.Select(td => td.ToDto()).ToList();
    }
}