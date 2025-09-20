using Immediate.Handlers.Shared;
using Immediate.Validations.Shared;
using Scalar.AspNetCore;
using TodoVSA.Api;
using TodoVSA.Api.Features.Todos.Controllers;

[assembly: Behaviors(
    typeof(ValidationBehavior<,>)
)]

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddTodoVSAApiHandlers();
builder.Services.AddTodoVSAApiBehaviors();

builder.Services.AddScoped<CreateTodoHandler>();
builder.Services.AddScoped<DeleteTodoHandler>();
builder.Services.AddScoped<CompleteTodoHandler>();
builder.Services.AddScoped<GetTodosHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt =>
    {
        opt.Title = "TodoVSA API";
        opt.DarkMode = true;
        opt.Theme = ScalarTheme.Mars;
        opt.BaseServerUrl = "http://localhost:5035";

    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapTodoVSAApiEndpoints();

app.Run();
