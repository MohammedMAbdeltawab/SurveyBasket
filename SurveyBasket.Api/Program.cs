using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Middlewares;
using SurveyBasket.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Services — configuration phase (builder.*)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI — service lifetimes (Section 03)
builder.Services.AddTransient<IOperationTransient, WindowsOsService>();
builder.Services.AddScoped<IOperationScoped, WindowsOsService>();
builder.Services.AddSingleton<IOperationSingleton, WindowsOsService>();

// Keyed services (Section 03)
builder.Services.AddKeyedTransient<IOperationTransient, WindowsOsService>("windows");
builder.Services.AddKeyedTransient<IOperationTransient, MacOsService>("macOs");

var app = builder.Build();

// Middleware pipeline — request phase (app.*)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomMiddleware();
app.UseHttpsRedirection();

// UseAuthorization() will be added in Section 07
app.MapControllers();

app.Run();
