using NextRef.Application;
using NextRef.Infrastructure;
using NextRef.Infrastructure.DataAccess.Migrations;

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


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();