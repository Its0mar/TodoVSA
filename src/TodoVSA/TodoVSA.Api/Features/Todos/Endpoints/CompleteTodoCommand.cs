using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoVSA.Api.Data;

namespace TodoVSA.Api.Features.Todos.Endpoints;

[Handler]
[MapPut("api/todos/{Id}"), Authorize]
public static partial class CompleteTodoCommand
{
    [Validate]
    public sealed partial record Command : IValidationTarget<Command>
    {
        [GreaterThan(0)]
        [FromRoute]
        public int Id { get; init; }
    }
    private static async ValueTask<bool> HandleAsync(Command command, AppDbContext context, CancellationToken token)
    {
        var todo = await context.ToDos.FirstOrDefaultAsync(t => t.Id == command.Id, token);
        if (todo is null) return false;
        
        todo.IsCompleted = true;
        context.ToDos.Update(todo);
        
        return await context.SaveChangesAsync(token) > 0;
    }
}