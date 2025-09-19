namespace TodoVSA.Api.Features.Todos.Models;

public record Todo(int Id, string Title, string Description, bool IsCompleted);