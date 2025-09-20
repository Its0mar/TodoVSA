using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoVSA.Api.Data;
using TodoVSA.Api.Data.Models;

namespace TodoVSA.Api.Features.Auth.Endpoints;

[Handler]
[MapPost("api/auth/register"), AllowAnonymous]
public static partial class RegisterCommand
{
    [Validate]
    public sealed partial record Command : IValidationTarget<Command>
    {
        [MinLength(1), MaxLength(30)]
        public required string Email { get; set; } 
        
        [MinLength(1), MaxLength(15)]
        public required string UserName { get; set; } 
        
        [MinLength(4), MaxLength(20)]
        public required string Password { get; set; }
        
    }
    private static async ValueTask<string> HandleAsync(Command command, AppDbContext context,
        IConfiguration configuration, UserManager<AppUser> userManager, CancellationToken token)
    {
        var user = new AppUser
        {
            Email = command.Email,
            UserName = command.UserName
        };
        var result = await userManager.CreateAsync(user, command.Password);
        if (result.Succeeded)
            return JwtTokenHelper.GenerateToken(user.Id, configuration );
        return string.Join("," ,result.Errors.Select(e => e.Description));

    }
}