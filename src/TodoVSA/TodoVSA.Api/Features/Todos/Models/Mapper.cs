namespace TodoVSA.Api.Features.Todos.Models;

public static class Mapper
{
    public static Todo ToDto(this Data.Todo todo) =>
        new (todo.Id, todo.Title, todo.Description, todo.IsCompleted);
}