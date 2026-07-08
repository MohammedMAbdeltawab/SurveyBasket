var builder = WebApplication.CreateBuilder(args);

// Services — configuration phase (builder.*)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPollService, PollService>();


var app = builder.Build();

// Middleware pipeline — request phase (app.*)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
