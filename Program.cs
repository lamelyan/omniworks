using Oakton;
using OmniWorks.Endpoints;
using OmniWorks.Infrastructure;
using OmniWorks.Middleware;
using Wolverine;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//global error handling 
builder.Services.AddTransient<GlobalErrorHandling>();

builder.Host.UseWolverine(opts => 
{
    //opts.Durability.Mode = DurabilityMode.MediatorOnly;
});

var app = builder.Build();

// app.UseMiddleware<GlobalErrorHandling>();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndpoints();

app.MapGet("/UnhandledExceptionTest", () =>
{
    throw new Exception("unhandled exception with lots of private data here");
});

await app.RunOaktonCommands(args);