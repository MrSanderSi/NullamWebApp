using Microsoft.EntityFrameworkCore;
using NullamWebApp.ApiService.Services;
using NullamWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
		builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});

builder.Services.AddProblemDetails();
builder.Services.AddDbContext<NullamDbContext>(options =>
{
    options.UseSqlServer("Server=localhost;Database=NullamDb;Trusted_Connection=True;TrustServerCertificate=True");
});

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<ParticipantService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseCors("AllowAll");
app.MapDefaultEndpoints();

app.MapControllers();
app.Run();