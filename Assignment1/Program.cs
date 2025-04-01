using Assignment1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPersonService, PersonService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rookie}/{action=Get}/{id?}"
);
app.MapControllerRoute(
    name: "nashtech",
    pattern: "NashTech/{controller=Rookie}/{action=Get}/{id?}"
);
app.MapControllers();

app.Run();
