using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoVSA.Api.Data;

namespace TodoVSA.Api.Features.Todos.Endpoints;

[Handler]
[MapDelete("api/todos/{Id}"), Authorize]
public static partial class DeleteTodoCommand
{
    [Validate]
    public partial record Command : IValidationTarget<Command>
    {
        [FromRoute]
        [GreaterThan(0)]
        public int Id { get; init; }
    }
    
    
    private static async ValueTask<Results<Ok,NotFound>> HandleAsync(Command _, AppDbContext context ,CancellationToken token)
    {
        var todo = await context.ToDos.FirstOrDefaultAsync(x => x.Id == _.Id, token);
        
        return todo is null ?  TypedResults.NotFound() : TypedResults.Ok();
        
    }
}