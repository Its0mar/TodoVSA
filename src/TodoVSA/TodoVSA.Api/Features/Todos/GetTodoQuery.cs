using System.ComponentModel.DataAnnotations;
using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TodoVSA.Api.Data;
using TodoVSA.Api.Features.Todos.Models;
using Todo = TodoVSA.Api.Features.Todos.Models.Todo;
using ValidationResult = Immediate.Validations.Shared.ValidationResult;

namespace TodoVSA.Api.Features.Todos;

[Handler]
[MapGet("api/todos/{Id}")]
public static partial class GetTodoQuery
{
    [Validate]
    public sealed partial record Query : IValidationTarget<Query>
    {
        [FromRoute]
        [GreaterThan(0)]
        public int Id { get; init; }
    }
    private static async ValueTask<Results<Ok<Todo>, NotFound>> HandleAsync(Query query, CancellationToken token)
    {
        var todo = AppDbContext.ToDos.FirstOrDefault(x => x.Id == query.Id);
        return todo is null ? TypedResults.NotFound() : TypedResults.Ok(todo.ToDto());
        
    }
}