using Microsoft.AspNetCore.Identity;
using NextRef.Application;
using NextRef.Infrastructure;
using NextRef.Infrastructure.DataAccess.Migrations;
using NextRef.Infrastructure.Identity;
using NextRef.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Call DbUpRunner
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
if (builder.Environment.IsDevelopment())
{
    DbUpRunner.Run(connectionString);
}

// Add services to the container
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// JWT Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddAuthorizationPolicies();
builder.Services.AddCustomCors();

builder.Services.AddControllers(); 



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseCustomCors();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await IdentityDataSeeder.SeedRolesAsync(roleManager);
}

app.Run();

// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJkNTQ0ZmRjZC1lYmIyLTRmZDAtYjc3NS0wOGRkOWVjODAyYzYiLCJ1bmlxdWVfbmFtZSI6IlJlZFNreSIsImp0aSI6IjU1OWY5YmY2LTI4NTUtNGUzOC1hMzdjLWY3YTY4NGRiNzcyNiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzQ4NTM3NzYwLCJpc3MiOiJOZXh0UmVmQVBJIiwiYXVkIjoiTmV4dFJlZkNsaWVudCJ9.SzfqFQp4YwXacolBIYTUcnNcpRG0rFC6RzKV8q-O6P4