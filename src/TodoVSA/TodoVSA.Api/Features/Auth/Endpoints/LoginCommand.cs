using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoVSA.Api.Data.Models;

namespace TodoVSA.Api.Features.Auth.Endpoints;

[Handler]
[MapPost("api/auth/login"), AllowAnonymous]
public static partial class LoginCommand
{
    [Validate]
    public sealed partial record Command : IValidationTarget<Command>
    {
        [MinLength(1), MaxLength(15)]
        public required string UserName { get; init; } 
        
        [MinLength(4), MaxLength(20)]
        public required string Password { get; init; }
    }
    private static async ValueTask<string> HandleAsync(Command command,IConfiguration configuration ,UserManager<AppUser> userManager,CancellationToken token)
    {
        var user = await userManager.FindByNameAsync(command.UserName);
        
        if (user != null && await userManager.CheckPasswordAsync(user, command.Password))
            return JwtTokenHelper.GenerateToken(user.Id, configuration );
        
        return string.Empty;
    }
}