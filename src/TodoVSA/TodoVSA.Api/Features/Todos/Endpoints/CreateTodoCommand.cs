using System.Security.Claims;
using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using TodoVSA.Api.Data;

namespace TodoVSA.Api.Features.Todos.Endpoints;

[Handler]
[MapPost("api/todos"), Authorize]

public static partial class CreateTodoCommand
{
    [Validate]
    public sealed partial record Command : IValidationTarget<Command>
    {
        [MinLength(1), MaxLength(30)]
        public required string Title { get; set; }
        
        [MaxLength(150)]
        public string? Description { get; set; }
    }
    private static async ValueTask<bool> HandleAsync(Command command, AppDbContext context, IHttpContextAccessor httpContextAccessor,CancellationToken token)
    {
        // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return false;
        
        var todo = new Todo()
        {
            Title = command.Title,
            Description = command.Description ?? string.Empty,
            IsCompleted = false,
            AppUserId = Int32.Parse(userId)
        };
        context.ToDos.Add(todo);
        
        return await context.SaveChangesAsync(token) > 0;
    }
}