using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TodoVSA.Api.Data;
using TodoVSA.Api.Features.Todos.Models;
using Todo = TodoVSA.Api.Features.Todos.Models.Todo;

namespace TodoVSA.Api.Features.Todos.Endpoints;

[Handler]
[MapGet("api/todos"), Authorize]

public static partial class GetTodosQuery
{
    public record struct Query;
    private static async ValueTask<IReadOnlyList<Todo>> HandleAsync(Query _, AppDbContext context, CancellationToken token)
    {
        var todos =  await context.ToDos.Select(t => t.ToDto()).ToListAsync(token);
        return todos;
    }
}