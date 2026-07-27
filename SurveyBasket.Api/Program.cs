



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Middleware pipeline — request phase (app.*)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   // who are you? — validates JWT, fills HttpContext.User
app.UseAuthorization();    // what can you do? — enforces [Authorize]

app.MapControllers();

app.Run();
