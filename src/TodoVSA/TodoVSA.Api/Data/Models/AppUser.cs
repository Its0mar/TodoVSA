using Microsoft.AspNetCore.Identity;

namespace TodoVSA.Api.Data.Models;

public class AppUser : IdentityUser<int>
{
    public List<Todo> Todos { get; set; }
}