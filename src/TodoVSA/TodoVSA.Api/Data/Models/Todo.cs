using TodoVSA.Api.Data.Models;

namespace TodoVSA.Api.Data;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
}