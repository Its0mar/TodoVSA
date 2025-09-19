namespace TodoVSA.Api.Data;

public static class AppDbContext
{
    public static List<Todo> ToDos { get; set; } = new();
}