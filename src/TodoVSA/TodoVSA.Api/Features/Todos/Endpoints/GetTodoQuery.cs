using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoVSA.Api.Data;
using TodoVSA.Api.Features.Todos.Models;
using Todo = TodoVSA.Api.Features.Todos.Models.Todo;

namespace TodoVSA.Api.Features.Todos;

[Handler]
[MapGet("api/todos/{Id}"), Authorize]
public static partial class GetTodoQuery
{
    [Validate]
    public sealed partial record Query : IValidationTarget<Query>
    {
        [FromRoute]
        [GreaterThan(0)]
        public int Id { get; init; }
    }
    private static async ValueTask<Results<Ok<Todo>, NotFound>> HandleAsync(Query query, AppDbContext context, CancellationToken token)
    {
        var todo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == query.Id, token);
        return todo is null ? TypedResults.NotFound() : TypedResults.Ok(todo.ToDto());
        
    }
}