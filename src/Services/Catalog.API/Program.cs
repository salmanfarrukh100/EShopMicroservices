var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(Config=> 
{
    Config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(Opts =>
{
    Opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
var app = builder.Build();
app.MapCarter();
// Configure the HTTP pipeline
app.Run();
