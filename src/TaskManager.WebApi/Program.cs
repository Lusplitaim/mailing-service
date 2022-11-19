using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManager.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
//builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.Services.AddScoped<DatabaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();

app.UseAuthorization();

app.UseCors((policyBuilder) =>
{
    policyBuilder.WithOrigins("http://localhost:4200")
    .AllowAnyHeader().AllowAnyMethod();
});

app.MapControllers();

app.Run();
