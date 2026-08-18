



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

// Middleware pipeline — request phase (app.*)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Skip HTTPS redirect in Development — otherwise Postman/curl on http://localhost:5005
// gets 307 → https and the Authorization header is dropped → 401.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();   // who are you? — validates JWT, fills HttpContext.User
app.UseAuthorization();    // what can you do? — enforces [Authorize]

app.MapControllers();

app.Run();
